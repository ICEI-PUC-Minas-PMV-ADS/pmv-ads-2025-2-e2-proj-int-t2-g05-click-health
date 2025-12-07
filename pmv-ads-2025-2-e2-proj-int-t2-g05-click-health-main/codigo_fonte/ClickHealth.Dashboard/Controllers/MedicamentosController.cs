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

        // 🔒 Verificação via Session
        private bool UsuarioNaoLogado()
        {
            return HttpContext.Session.GetInt32("UserId") == null;
        }

        // GET: /Medicamentos
        public async Task<IActionResult> Index()
        {
            if (UsuarioNaoLogado())
                return RedirectToAction("Login", "Account");

            var medicamentos = await _context.Medicacoes
                .OrderBy(m => m.Nome)
                .ToListAsync();

            return View(medicamentos);
        }

        // GET: /Medicamentos/Adicionar
        [HttpGet("Adicionar")]
        public IActionResult Adicionar()
        {
            if (UsuarioNaoLogado())
                return RedirectToAction("Login", "Account");

            return View(new Medicacao { IdPaciente = 1 });
        }

        // POST: /Medicamentos/Adicionar
        [HttpPost("Adicionar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adicionar(Medicacao medicacao)
        {
            if (UsuarioNaoLogado())
                return RedirectToAction("Login", "Account");

            if (medicacao.IdPaciente == 0)
                medicacao.IdPaciente = 1;

            ModelState.Remove("Paciente");

            if (ModelState.IsValid)
            {
                _context.Medicacoes.Add(medicacao);
                await _context.SaveChangesAsync();
                TempData["Sucesso"] = "Medicamento adicionado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

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
            if (UsuarioNaoLogado())
                return RedirectToAction("Login", "Account");

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
            if (UsuarioNaoLogado())
                return RedirectToAction("Login", "Account");

            if (id != medicacao.IdMedicacao)
                return BadRequest();

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
            if (UsuarioNaoLogado())
                return RedirectToAction("Login", "Account");

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
            if (UsuarioNaoLogado())
                return RedirectToAction("Login", "Account");

            var medicacao = await _context.Medicacoes
                .Include(m => m.Agendamentos)
                .FirstOrDefaultAsync(m => m.IdMedicacao == id);

            if (medicacao != null)
            {
                _context.AgendamentoMedicacao.RemoveRange(medicacao.Agendamentos);
                _context.Medicacoes.Remove(medicacao);
                await _context.SaveChangesAsync();

                TempData["Sucesso"] = "Medicamento e agendamentos relacionados excluídos com sucesso!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
