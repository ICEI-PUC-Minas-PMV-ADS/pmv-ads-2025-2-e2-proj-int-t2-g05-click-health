// Arquivo: Controllers/FeedController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClickHealth.Dashboard.Models;
using ClickHealth.Dashboard.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace ClickHealth.Dashboard.Controllers
{
    public class FeedController : Controller
    {
        private readonly ClickHealthContext _context;

        public FeedController(ClickHealthContext context)
        {
            _context = context;
        }

        // 🔒 Verificação via Session
        private bool UsuarioNaoLogado()
        {
            return HttpContext.Session.GetInt32("UserId") == null;
        }

        // GET: /Feed
        public async Task<IActionResult> Index()
        {
            if (UsuarioNaoLogado())
                return RedirectToAction("Login", "Account");

            var viewModel = new FeedViewModel();
            var dataLimite = DateTime.Now.AddDays(7);

            // Agendamentos: Próximos 7 dias (AgendamentoMedicacao)
            viewModel.AgendamentosRecentes = await _context.Set<AgendamentoMedicacao>()
                .Where(a => a.DataHora >= DateTime.Now && a.DataHora <= dataLimite)
                .OrderBy(a => a.DataHora)
                .ToListAsync();

            // Medicamentos ativos
            viewModel.MedicamentosAtivos = await _context.Set<Medicacao>()
                .Where(m => m.DataFim == null || m.DataFim >= DateTime.Now)
                .OrderBy(m => m.Nome)
                .ToListAsync();

            // Últimos 10 alertas
            viewModel.Alertas = await _context.Set<Alerta>()
                .OrderByDescending(a => a.DataHora)
                .Take(10)
                .ToListAsync();

            // 5 registros de histórico médico mais recentes
            viewModel.HistoricoMedicoRecente = await _context.Set<HistoricoMedico>()
                .OrderByDescending(h => h.AtualizadoEm)
                .Take(5)
                .ToListAsync();

            viewModel.MensagemStatus =
                $"Bem-vindo(a) ao seu Feed! Você tem {viewModel.AgendamentosRecentes.Count()} agendamentos próximos.";

            return View(viewModel);
        }
    }
}
