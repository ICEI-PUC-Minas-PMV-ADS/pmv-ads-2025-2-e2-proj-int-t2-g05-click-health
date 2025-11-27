using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClickHealth.Dashboard.Models;

namespace ClickHealth.Dashboard.Controllers
{
    [Route("Medicamentos")]
    public class MedicamentosController : Controller
    {
        private readonly ClickHealthContext _context;

        public MedicamentosController(ClickHealthContext context)
        {
            _context = context;
        }

        // GET: /Medicamentos
        public async Task<IActionResult> Index()
        {
            var medicamentos = await _context.Medicacoes
                .OrderBy(m => m.Nome)
                .ToListAsync();

            return View(medicamentos);
        }

        // GET: /Medicamentos/Adicionar
        [HttpGet("Adicionar")]
        public IActionResult Adicionar()
        {
            return View(new Medicacao { IdPaciente = 1 });
        }

        // POST: /Medicamentos/Adicionar
        [HttpPost("Adicionar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adicionar(Medicacao medicacao)
        {
            // 1. Garante que o IdPaciente seja preenchido (para fins de teste)
            if (medicacao.IdPaciente == 0)
            {
                medicacao.IdPaciente = 1;
            }

            // 2. Remove o erro de validação da propriedade de navegação Paciente
            ModelState.Remove("Paciente");

            if (ModelState.IsValid)
            {
                _context.Medicacoes.Add(medicacao);
                await _context.SaveChangesAsync();
                TempData["Sucesso"] = "Medicamento adicionado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            // 3. Exibe mensagem de erro se a validação falhar
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (errors.Any())
            {
                TempData["Erro"] = "Falha na validação do formulário. Verifique os campos e tente novamente.";
            }

            return View(medicacao);
        }

        // GET: /Medicamentos/Editar/5
        [HttpGet("Editar/{id:int}")]
        public async Task<IActionResult> Editar(int id)
        {
            var medicacao = await _context.Medicacoes
                .FirstOrDefaultAsync(m => m.IdMedicacao == id);

            if (medicacao == null)
                return NotFound();

            return View(medicacao);
        }

        // POST: /Medicamentos/Editar/5
        [HttpPost("Editar/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Medicacao medicacao)
        {
            if (id != medicacao.IdMedicacao)
                return BadRequest();

            // Remove a validação da propriedade de navegação
            ModelState.Remove("Paciente");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicacao);
                    await _context.SaveChangesAsync();
                    TempData["Sucesso"] = "Medicamento atualizado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Medicacoes.AnyAsync(m => m.IdMedicacao == id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(medicacao);
        }

        // GET: /Medicamentos/Excluir/5
        [HttpGet("Excluir/{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var medicacao = await _context.Medicacoes
                .Include(m => m.Paciente)
                .FirstOrDefaultAsync(m => m.IdMedicacao == id);

            if (medicacao == null)
                return NotFound();

            return View(medicacao);
        }

        // POST: /Medicamentos/Excluir/5
        [HttpPost("Excluir/{id:int}"), ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirConfirmado(int id)
        {
            var medicacao = await _context.Medicacoes
                .Include(m => m.Agendamentos) // Inclui os agendamentos relacionados
                .FirstOrDefaultAsync(m => m.IdMedicacao == id);

            if (medicacao != null)
            {
                // 1. Remove todos os agendamentos relacionados (Ação em Cascata Manual)
                _context.AgendamentoMedicacao.RemoveRange(medicacao.Agendamentos);

                // 2. Remove o medicamento
                _context.Medicacoes.Remove(medicacao);

                // 3. Salva as alterações
                await _context.SaveChangesAsync();
                TempData["Sucesso"] = "Medicamento e seus agendamentos relacionados excluídos com sucesso!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}