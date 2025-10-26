using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClickHealth.Models;
using System.Threading.Tasks;

namespace ClickHealth.Controllers
{
    public class MedicamentosTomadosController : Controller
    {
        private readonly AppDbContext _context;

        public MedicamentosTomadosController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var registros = await _context.MedicamentosTomados
                .Include(m => m.Medicamento)
                .ToListAsync();

            return View(registros);
        }

        public IActionResult Create()
        {
            ViewBag.Medicamentos = _context.Medicamentos.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicamentoTomado registro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Medicamentos = _context.Medicamentos.ToList();
            return View(registro);
        }
    }
}
