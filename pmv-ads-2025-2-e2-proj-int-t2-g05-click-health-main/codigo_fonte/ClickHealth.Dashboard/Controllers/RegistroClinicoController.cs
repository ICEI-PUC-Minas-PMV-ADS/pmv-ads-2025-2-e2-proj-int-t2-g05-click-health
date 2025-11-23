using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClickHealth.Dashboard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClickHealth.Dashboard.Controllers
{
    public class RegistroClinicoController : Controller
    {
        private readonly ClickHealthContext _context;

        public RegistroClinicoController(ClickHealthContext context)
        {
            _context = context;
        }

        // 📚 LISTA / HISTÓRICO – cards
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var registros = await _context.RegistrosClinicos
                .Include(r => r.Paciente)
                .OrderByDescending(r => r.DataRegistro)
                .ToListAsync();

            return View(registros);
        }

        // 👁 DETALHES de um registro
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var registro = await _context.RegistrosClinicos
                .Include(r => r.Paciente)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (registro == null)
                return NotFound();

            return View(registro);
        }

        // 📝 GET – NOVO REGISTRO
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await CarregarPacientesAsync();

            var model = new RegistroClinico
            {
                DataRegistro = DateTime.Now
            };

            return View(model);
        }

        // 📝 POST – SALVAR REGISTRO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistroClinico model, IFormFile? arquivoExame)
        {
            if (!ModelState.IsValid)
            {
                await CarregarPacientesAsync();
                return View(model);
            }

            // Data do registro sempre atual
            model.DataRegistro = DateTime.Now;

            // Upload de arquivo (opcional)
            if (arquivoExame != null && arquivoExame.Length > 0)
            {
                var uploadsPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot", "uploads", "registros"
                );

                Directory.CreateDirectory(uploadsPath);

                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(arquivoExame.FileName)}";
                var filePath = Path.Combine(uploadsPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await arquivoExame.CopyToAsync(stream);
                }

                model.ArquivoPath = "/uploads/registros/" + fileName;
            }

            _context.RegistrosClinicos.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // 🔧 helper para carregar dropdown de pacientes
        private async Task CarregarPacientesAsync()
        {
            var pacientes = await _context.Pacientes
                .OrderBy(p => p.DadosPessoais)
                .ToListAsync();

            ViewBag.Pacientes = new SelectList(
                pacientes,
                nameof(Paciente.IdPaciente),
                nameof(Paciente.DadosPessoais)
            );
        }
    }
}
