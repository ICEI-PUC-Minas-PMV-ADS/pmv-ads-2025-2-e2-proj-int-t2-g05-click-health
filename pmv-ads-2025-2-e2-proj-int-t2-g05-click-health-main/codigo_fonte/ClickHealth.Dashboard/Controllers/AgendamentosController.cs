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

        // GET: /Agenda → Lista todos os agendamentos (pendentes e agendados)
        public async Task<IActionResult> Index()
        {
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
            await CarregarDropdownsAsync();
            return View(new AgendamentoMedicacao());
        }

        // POST: /Agenda/Adicionar
        [HttpPost("Adicionar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adicionar(AgendamentoMedicacao agendamento)
        {
            // Correção de Validação: Remove a validação das propriedades de navegação
            // Paciente e Medicacao, que são obrigatórias no modelo, mas não são enviadas pelo formulário.
            ModelState.Remove("Paciente");
            ModelState.Remove("Medicacao");

            if (ModelState.IsValid)
            {
                agendamento.Status = "Pendente";
                agendamento.DataCadastro = DateTime.Now;

                _context.AgendamentoMedicacao.Add(agendamento);
                await _context.SaveChangesAsync();

                TempData["Sucesso"] = "Agendamento criado com sucesso!"; // Alterado para "Sucesso" para usar o _Layout
                return RedirectToAction(nameof(Index));
            }

            // Adiciona mensagem de erro para o usuário se a validação falhar por outro motivo
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (errors.Any())
            {
                TempData["Erro"] = "Falha na validação do formulário de agendamento. Verifique os campos e tente novamente.";
            }

            await CarregarDropdownsAsync();
            return View(agendamento);
        }

        // GET: /Agenda/Editar/5
        [HttpGet("Editar/{id:int}")]
        public async Task<IActionResult> Editar(int id)
        {
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
            if (id != agendamento.Id)
                return BadRequest();

            // Correção de Validação: Remove a validação das propriedades de navegação
            ModelState.Remove("Paciente");
            ModelState.Remove("Medicacao");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agendamento);
                    await _context.SaveChangesAsync();
                    TempData["Sucesso"] = "Agendamento atualizado com sucesso!"; // Alterado para "Sucesso"
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
            var agendamento = await _context.AgendamentoMedicacao.FindAsync(id);
            if (agendamento != null)
            {
                _context.AgendamentoMedicacao.Remove(agendamento);
                await _context.SaveChangesAsync();
                TempData["Sucesso"] = "Agendamento excluído com sucesso!"; // Alterado para "Sucesso"
            }

            return RedirectToAction(nameof(Index));
        }

        // Carrega pacientes e medicações nos dropdowns
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
