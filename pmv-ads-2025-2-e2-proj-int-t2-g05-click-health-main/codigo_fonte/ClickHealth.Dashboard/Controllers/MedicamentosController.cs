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
            return View(new Medicacao());
        }

        // POST: /Medicamentos/Adicionar
        [HttpPost("Adicionar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adicionar(Medicacao medicacao)
        {
            if (ModelState.IsValid)
            {
                medicacao.DataCadastro = DateTime.Now;
                _context.Medicacoes.Add(medicacao);
                await _context.SaveChangesAsync();
                TempData["Sucesso"] = "Medicamento adicionado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            return View(medicacao);
        }
    }
}