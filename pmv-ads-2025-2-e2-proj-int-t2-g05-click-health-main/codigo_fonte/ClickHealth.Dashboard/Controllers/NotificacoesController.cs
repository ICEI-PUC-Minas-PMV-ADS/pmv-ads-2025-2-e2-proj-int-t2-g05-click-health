using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClickHealth.Dashboard.Controllers;
using ClickHealth.Dashboard.Models;

namespace ClickHealth.Controllers
{
    public class NotificacoesController : Controller
    {
        private readonly ClickHealthContext _context;

        public NotificacoesController(ClickHealthContext context)
        {
            _context = context;
        }

        // ✅ PÁGINA PRINCIPAL — lista notificações conforme tipo de usuário
        public IActionResult Index()
        {
            // Recupera sessão
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            bool isAdmin = HttpContext.Session.GetString("IsAdmin") == "true";

            // Base da consulta
            IQueryable<Notificacao> query = _context.Notificacoes
                .Include(n => n.Paciente)
                .OrderByDescending(n => n.DataHora);

            // 🔥 SE NÃO FOR ADMIN → filtra apenas notificações do paciente do usuário logado
            if (!isAdmin)
            {
                query = query.Where(n =>
                    n.Paciente != null &&
                    n.Paciente.IdUsuario == userId.Value);
            }

            var notificacoes = query.ToList();

            // Evita erro se o paciente estiver nulo
            foreach (var n in notificacoes)
            {
                if (n.Paciente == null)
                    n.Paciente = new Paciente { DadosPessoais = "(sem nome)" };
            }

            return View(notificacoes);
        }

        // ✅ CREATE (GET)
        public IActionResult Create()
        {
            ViewBag.IdPaciente = new SelectList(_context.Pacientes, "IdPaciente", "DadosPessoais");
            return View(new Notificacao());
        }

        // ✅ CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Notificacao notificacao)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IdPaciente = new SelectList(_context.Pacientes, "IdPaciente", "DadosPessoais", notificacao.IdPaciente);
                return View(notificacao);
            }

            notificacao.DataHora = notificacao.DataHora == default ? DateTime.Now : notificacao.DataHora;
            notificacao.Lida = false;

            try
            {
                _context.Notificacoes.Add(notificacao);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao salvar: " + ex.Message);
                ViewBag.IdPaciente = new SelectList(_context.Pacientes, "IdPaciente", "DadosPessoais", notificacao.IdPaciente);
                return View(notificacao);
            }
        }

        // ✅ EDIT (GET)
        public IActionResult Edit(int id)
        {
            var notificacao = _context.Notificacoes.Find(id);
            if (notificacao == null)
                return NotFound();

            ViewBag.IdPaciente = new SelectList(_context.Pacientes, "IdPaciente", "DadosPessoais", notificacao.IdPaciente);
            return View(notificacao);
        }

        // ✅ EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Notificacao notificacao)
        {
            if (id != notificacao.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.IdPaciente = new SelectList(_context.Pacientes, "IdPaciente", "DadosPessoais", notificacao.IdPaciente);
                return View(notificacao);
            }

            try
            {
                _context.Update(notificacao);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Notificacoes.Any(e => e.Id == notificacao.Id))
                    return NotFound();
                throw;
            }
        }

        // ✅ MARCAR COMO LIDA (individual)
        public IActionResult MarcarComoLida(int id)
        {
            var notif = _context.Notificacoes.Find(id);
            if (notif != null)
            {
                notif.Lida = true;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // ✅ MARCAR TODAS COMO LIDAS
        public IActionResult MarcarTodasComoLidas()
        {
            var notificacoes = _context.Notificacoes.Where(n => !n.Lida).ToList();
            if (notificacoes.Any())
            {
                foreach (var n in notificacoes)
                    n.Lida = true;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // ✅ DELETE (GET)
        public IActionResult Delete(int id)
        {
            var notificacao = _context.Notificacoes
                .Include(n => n.Paciente)
                .FirstOrDefault(n => n.Id == id);
            if (notificacao == null)
                return NotFound();

            return View(notificacao);
        }

        // ✅ DELETE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var notificacao = _context.Notificacoes.Find(id);
            if (notificacao != null)
            {
                _context.Notificacoes.Remove(notificacao);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // ✅ DETAILS
        public IActionResult Details(int id)
        {
            var notificacao = _context.Notificacoes
                .Include(n => n.Paciente)
                .FirstOrDefault(n => n.Id == id);
            if (notificacao == null)
                return NotFound();

            return View(notificacao);
        }
    }
}
