using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClickHealth.Dashboard.Models;
using ClickHealth.Dashboard.Models.ViewModels;

namespace ClickHealth.Dashboard.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ClickHealthContext _context;

        public UsuariosController(ClickHealthContext context)
        {
            _context = context;
        }

        // ==========================================
        // LISTA DE USUÁRIOS / PACIENTES
        // ==========================================
        public IActionResult Index()
        {
            var lista = _context.Pacientes
                .Include(p => p.IdUsuarioNavigation)
                .ToList();

            return View(lista);
        }

        // ==========================================
        // DETALHES
        // ==========================================
        public IActionResult Details(long id)
        {
            var paciente = _context.Pacientes
                .Include(p => p.IdUsuarioNavigation)
                .FirstOrDefault(p => p.IdPaciente == id);

            if (paciente == null)
                return NotFound();

            return View(paciente);
        }

        // ==========================================
        // CRIAÇÃO: GET
        // ==========================================
        [HttpGet]
        public IActionResult Create()
        {
            return View(new UsuariosCreateViewModel());
        }

        // ==========================================
        // CRIAÇÃO: POST
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UsuariosCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Senha != model.ConfirmarSenha)
            {
                ModelState.AddModelError("ConfirmarSenha", "As senhas não coincidem.");
                return View(model);
            }

            // Criar usuário
            var usuario = new Usuario
            {
                Email = model.Email,
                SenhaHash = model.Senha,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            // Criar paciente vinculado
            var paciente = new Paciente
            {
                IdUsuario = usuario.IdUsuario,
                DadosPessoais = model.DadosPessoais,
                DataNascimento = model.DataNascimento,
                Telefone = "",
                CondicoesMedicas = ""
            };

            _context.Pacientes.Add(paciente);
            _context.SaveChanges();

            return RedirectToAction("Login", "Account");
        }

        // ==========================================
        // EDITAR
        // ==========================================
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var paciente = _context.Pacientes
                .Include(p => p.IdUsuarioNavigation)
                .FirstOrDefault(p => p.IdPaciente == id);

            if (paciente == null)
                return NotFound();

            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, Paciente pacienteForm)
        {
            if (id != pacienteForm.IdPaciente)
                return NotFound();

            if (!ModelState.IsValid)
                return View(pacienteForm);

            var paciente = _context.Pacientes.Find(id);
            if (paciente == null)
                return NotFound();

            paciente.DadosPessoais = pacienteForm.DadosPessoais;
            paciente.DataNascimento = pacienteForm.DataNascimento;
            paciente.CondicoesMedicas = pacienteForm.CondicoesMedicas;
            paciente.Telefone = pacienteForm.Telefone;

            _context.Update(paciente);
            _context.SaveChanges();

            return RedirectToAction(nameof(Details), new { id });
        }

        // ==========================================
        // DELETE
        // ==========================================
        [HttpGet]
        public IActionResult Delete(long id)
        {
            var paciente = _context.Pacientes
               .Include(p => p.IdUsuarioNavigation)
               .FirstOrDefault(p => p.IdPaciente == id);

            if (paciente == null)
                return NotFound();

            return View(paciente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var paciente = _context.Pacientes.Find(id);
            if (paciente == null)
                return NotFound();

            var usuario = _context.Usuarios.Find(paciente.IdUsuario);

            _context.Pacientes.Remove(paciente);

            if (usuario != null)
                _context.Usuarios.Remove(usuario);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
