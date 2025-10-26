using Microsoft.AspNetCore.Mvc;
using ClickHealth.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ClickHealth.Controllers
{
    public class AgendamentosController : Controller
    {
        private readonly AppDbContext _context;

        public AgendamentosController(AppDbContext context)
        {
            _context = context;
        }

        // Listar todos os agendamentos
        public async Task<IActionResult> Index()
        {
            var lista = await _context.Agendamentos.ToListAsync();
            return View(lista);
        }

        // GET: Criar novo agendamento
        public IActionResult Create()
        {
            return View();
        }

        // POST: Criar novo agendamento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agendamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agendamento);
        }
    }
}
