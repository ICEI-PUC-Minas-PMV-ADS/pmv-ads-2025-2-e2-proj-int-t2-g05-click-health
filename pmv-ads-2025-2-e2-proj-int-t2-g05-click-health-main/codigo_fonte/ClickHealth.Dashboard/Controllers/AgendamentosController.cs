using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClickHealth.Dashboard.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace ClickHealth.Dashboard.Controllers
{
    [Route("Agenda")]
    public class AgendamentosController : Controller
    {
        private readonly ClickHealthContext _context;

        public AgendamentosController(ClickHealthContext context)
        {
            _context = context;
        }

        // 🔒 Verificação via Session
        private bool UsuarioNaoLogado()
        {
            return HttpContext.Session.GetInt32("UserId") == null;
        }

        // GET: /Agenda
        public async Task<IActionResult> Index()
        {
            if (UsuarioNaoLogado())
                return RedirectToAction("Login", "Account");

            var agendamentos = await _context.AgendamentoMedicacao
                .Include(a => a.Paciente)
                .Include(a => a.Medicacao)
                .Where(a => a.Status == "Pendente" || a.Status == "Agendado")
                .OrderBy(a => a.DataHora)
                .ToListAsync();

            return View(agendamentos);
        }

        // GET: /Agenda/Adicionar
        [HttpGet("Adicionar")]
        public async Task<IActionResult> Adicionar()
        {
            if (UsuarioNaoLogado())
                return RedirectToAction("Login", "Account");

            await CarregarDropdownsAsync();
            return View(new AgendamentoMedicacao());
        }

        // POST: /Agenda/Adicionar
        [HttpPost("Adicionar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adicionar(AgendamentoMedicacao agendamento)
        {
            if (UsuarioNaoLogado())
                return RedirectToAction("Login", "Account");

            ModelState.Remove("Paciente");
            ModelState.Remove("Medicacao");

            if (ModelState.IsValid)
            {
                agendamento.Status = "Pendente";
                agendamento.DataCadastro = DateTime.Now;

                _context.AgendamentoMedicacao.Add(agendamento);
                await _context.SaveChangesAsync();

                TempData["Sucesso"] = "Agendamento criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (errors.Any())
                TempData["Erro"] = "Falha na validação do formulário de agendamento.";

            await CarregarDropdownsAsync();
            return View(agendamento);
        }

        // GET: /Agenda/Editar/5
        [HttpGet("Editar/{id:int}")]
        public async Task<IActionResult> Editar(int id)
        {
            if (UsuarioNaoLogado())
                return RedirectToAction("Login", "Account");

            var agendamento = await _context.AgendamentoMedicacao
                .FirstOrDefaultAsync(a => a.Id == id);

            if (agendamento == null)
                return NotFound();

            await CarregarDropdownsAsync();
            return View(agendamento);
        }

        // POST: /Agenda/Editar/5
        [HttpPost("Editar/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, AgendamentoMedicacao agendamento)
        {
            if (UsuarioNaoLogado())
                return RedirectToAction("Login", "Account");

            if (id != agendamento.Id)
                return BadRequest();

            ModelState.Remove("Paciente");
            ModelState.Remove("Medicacao");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agendamento);
                    await _context.SaveChangesAsync();
                    TempData["Sucesso"] = "Agendamento atualizado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.AgendamentoMedicacao.AnyAsync(a => a.Id == id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            await CarregarDropdownsAsync();
            return View(agendamento);
        }

        // GET: /Agenda/Excluir/5
        [HttpGet("Excluir/{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            if (UsuarioNaoLogado())
                return RedirectToAction("Login", "Account");

            var agendamento = await _context.AgendamentoMedicacao
                .Include(a => a.Paciente)
                .Include(a => a.Medicacao)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (agendamento == null)
                return NotFound();

            return View(agendamento);
        }

        // POST: /Agenda/Excluir/5
        [HttpPost("Excluir/{id:int}"), ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirConfirmado(int id)
        {
            if (UsuarioNaoLogado())
                return RedirectToAction("Login", "Account");

            var agendamento = await _context.AgendamentoMedicacao.FindAsync(id);

            if (agendamento != null)
            {
                _context.AgendamentoMedicacao.Remove(agendamento);
                await _context.SaveChangesAsync();
                TempData["Sucesso"] = "Agendamento excluído com sucesso!";
            }

            return RedirectToAction(nameof(Index));
        }

        // Dropdowns
        private async Task CarregarDropdownsAsync()
        {
            ViewBag.Pacientes = await _context.Pacientes
                .OrderBy(p => p.DadosPessoais)
                .Select(p => new SelectListItem
                {
                    Value = p.IdPaciente.ToString(),
                    Text = p.DadosPessoais ?? "Sem nome"
                })
                .ToListAsync();

            ViewBag.Medicacoes = await _context.Medicacoes
                .Include(m => m.Paciente)
                .OrderBy(m => m.Nome)
                .Select(m => new SelectListItem
                {
                    Value = m.IdMedicacao.ToString(),
                    Text = $"{m.Nome} - {m.Dosagem ?? "Sem dosagem"} {(m.Paciente != null ? $"({m.Paciente.DadosPessoais})" : "")}".Trim()
                })
                .ToListAsync();
        }
    }
}
