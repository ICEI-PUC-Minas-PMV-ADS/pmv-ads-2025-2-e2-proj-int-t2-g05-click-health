using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClickHealth.Dashboard.Models;

namespace ClickHealth.Controllers
{
    public class AlertasController : Controller
    {
        private readonly ClickHealthContext _context;

        public AlertasController(ClickHealthContext context)
        {
            _context = context;
        }

        // GET: Alertas
        public async Task<IActionResult> Index()
        {
            // Inclui o Paciente para exibir o nome e ordena por DataHora (mais recentes primeiro)
            var alertas = _context.Alertas
                .Include(a => a.IdPacienteNavigation)
                .OrderByDescending(a => a.DataHora);

            return View(await alertas.ToListAsync());
        }

        // GET: Alertas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alerta = await _context.Alertas
                .Include(a => a.IdPacienteNavigation)
                .FirstOrDefaultAsync(m => m.IdAlerta == id);

            if (alerta == null)
            {
                return NotFound();
            }

            return View(alerta);
        }

        // GET: Alertas/Create
        public IActionResult Create()
        {
            ViewData["IdPaciente"] = new SelectList(
                _context.Pacientes.OrderBy(p => p.DadosPessoais),
                "IdPaciente",
                "DadosPessoais"
            );
            return View();
        }

        // POST: Alertas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAlerta,IdPaciente,Titulo,Mensagem,Tipo,Status,DataHora")] Alerta alerta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alerta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdPaciente"] = new SelectList(
                _context.Pacientes.OrderBy(p => p.DadosPessoais),
                "IdPaciente",
                "DadosPessoais",
                alerta.IdPaciente
            );

            return View(alerta);
        }

        // GET: Alertas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alerta = await _context.Alertas.FindAsync(id);
            if (alerta == null)
            {
                return NotFound();
            }

            ViewData["IdPaciente"] = new SelectList(
                _context.Pacientes.OrderBy(p => p.DadosPessoais),
                "IdPaciente",
                "DadosPessoais",
                alerta.IdPaciente
            );

            return View(alerta);
        }

        // POST: Alertas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAlerta,IdPaciente,Titulo,Mensagem,Tipo,Status,DataHora")] Alerta alerta)
        {
            if (id != alerta.IdAlerta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alerta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlertaExists(alerta.IdAlerta))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdPaciente"] = new SelectList(
                _context.Pacientes.OrderBy(p => p.DadosPessoais),
                "IdPaciente",
                "DadosPessoais",
                alerta.IdPaciente
            );

            return View(alerta);
        }

        // GET: Alertas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alerta = await _context.Alertas
                .Include(a => a.IdPacienteNavigation)
                .FirstOrDefaultAsync(m => m.IdAlerta == id);

            if (alerta == null)
            {
                return NotFound();
            }

            return View(alerta);
        }

        // POST: Alertas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alerta = await _context.Alertas.FindAsync(id);
            if (alerta != null)
            {
                _context.Alertas.Remove(alerta);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AlertaExists(int id)
        {
            return _context.Alertas.Any(e => e.IdAlerta == id);
        }
    }
}
