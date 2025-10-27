using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using mf_dev_backend_2025.Models;

namespace mf_dev_backend_2025.Controllers
{
    public class NovosPacientesController : Controller
    {
        private readonly AppBbContex _context;
        private readonly IWebHostEnvironment _env;

        public NovosPacientesController(AppBbContex context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: /NovosPacientes
        public async Task<IActionResult> Index()
        {
            // Lista simples (sem tracking) ordenada por Id desc
            var dados = await _context.novospacientes
                .AsNoTracking()
                .OrderByDescending(p => p.Id)
                .ToListAsync();

            return View(dados);
        }

        // GET: /NovosPacientes/Create
        public IActionResult Create() => View();

        // POST: /NovosPacientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(novopaciente model, IFormFile? foto)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                // Salva a foto (se houver) e grava o caminho no próprio model
                model.FotoPath = await SaveFotoAsync(foto);
            }
            catch (InvalidOperationException ex)
            {
                // Erros de validação do arquivo (tamanho/tipo)
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }

            _context.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /NovosPacientes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0) return NotFound();

            var paciente = await _context.novospacientes
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (paciente == null) return NotFound();

            return View(paciente);
        }

        // GET: /NovosPacientes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0) return NotFound();

            var paciente = await _context.novospacientes.FindAsync(id);
            if (paciente == null) return NotFound();

            return View(paciente);
        }

        // POST: /NovosPacientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, novopaciente formModel, IFormFile? foto)
        {
            if (id != formModel.Id) return BadRequest();
            if (!ModelState.IsValid) return View(formModel);

            var paciente = await _context.novospacientes.FindAsync(id);
            if (paciente == null) return NotFound();

            // Atualiza campos simples
            paciente.Nome = formModel.Nome;
            paciente.email = formModel.email;

            // Atualiza senha apenas se foi informada
            if (!string.IsNullOrWhiteSpace(formModel.senha))
                paciente.senha = formModel.senha;

            // Troca de foto (se veio arquivo novo)
            if (foto is not null && foto.Length > 0)
            {
                try
                {
                    var newPath = await SaveFotoAsync(foto);

                    // Apaga a imagem antiga com segurança
                    if (!string.IsNullOrEmpty(paciente.FotoPath))
                        DeleteFotoIfLocal(paciente.FotoPath);

                    paciente.FotoPath = newPath;
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(formModel);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /NovosPacientes/Delete/5   (tela de confirmação)
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return NotFound();

            var paciente = await _context.novospacientes
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (paciente == null) return NotFound();

            return View(paciente);
        }

        // POST: /NovosPacientes/Delete/5  (confirma exclusão)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.novospacientes.FindAsync(id);
            if (paciente == null) return NotFound();

            // Remove a foto do disco (se estiver em /uploads)
            if (!string.IsNullOrEmpty(paciente.FotoPath))
                DeleteFotoIfLocal(paciente.FotoPath);

            _context.novospacientes.Remove(paciente);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ============ Helpers ============

        // Salva a foto e retorna o caminho relativo (/uploads/xxx.ext)
        private async Task<string?> SaveFotoAsync(IFormFile? foto)
        {
            if (foto is null || foto.Length == 0) return null;

            const long maxBytes = 10L * 1024 * 1024; // 10 MB
            var okTypes = new[] { "image/jpeg", "image/png", "image/webp", "image/gif" };

            if (foto.Length > maxBytes)
                throw new InvalidOperationException("A imagem deve ter no máximo 10 MB.");

            if (!okTypes.Contains(foto.ContentType))
                throw new InvalidOperationException("Formato inválido. Use JPG, PNG, WEBP ou GIF.");

            var uploadsDir = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsDir);

            var ext = Path.GetExtension(foto.FileName);
            var safeExt = string.IsNullOrWhiteSpace(ext) ? ".bin" : ext.ToLowerInvariant();
            var fileName = $"{Guid.NewGuid():N}{safeExt}";
            var fullPath = Path.Combine(uploadsDir, fileName);

            using (var fs = System.IO.File.Create(fullPath))
                await foto.CopyToAsync(fs);

            // Caminho que o navegador acessa
            return $"/uploads/{fileName}";
        }

        // Apaga a foto do disco (somente se o caminho estiver dentro de /uploads)
        private void DeleteFotoIfLocal(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath)) return;

            var full = Path.Combine(
                _env.WebRootPath,
                relativePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar)
            );

            var uploadsRoot = Path.Combine(_env.WebRootPath, "uploads");

            if (full.StartsWith(uploadsRoot, StringComparison.OrdinalIgnoreCase) &&
                System.IO.File.Exists(full))
            {
                System.IO.File.Delete(full);
            }
        }
    }
}
