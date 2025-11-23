using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClickHealth.Dashboard.Models;
using System.Linq;

namespace ClickHealth.Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClickHealthContext _context;

        public HomeController(ClickHealthContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // pega o usuário logado da sessão
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            // busca o paciente logado
            var usuario = await _context.Pacientes
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.IdPaciente == userId.Value);

            if (usuario == null)
                return RedirectToAction("Login", "Account");

            var viewModel = new DashboardViewModel
            {
                NomeUsuarioLogado = usuario.DadosPessoais ?? "Paciente"
            };

            // pega os últimos 5 pacientes cadastrados
            var pacientesDoBanco = await _context.Pacientes
                .OrderByDescending(p => p.IdPaciente != userId.Value)
                .Take(5)
                .ToListAsync();

            bool alternar = true;
            foreach (var paciente in pacientesDoBanco)
            {
                var pvm = new PacienteViewModel
                {
                    IdPaciente = paciente.IdPaciente,
                    NomePaciente = paciente.DadosPessoais ?? "Paciente",
                    ProximaAcao = paciente.CondicoesMedicas ?? "Nenhuma condição registrada",
                    StatusIndicador = alternar ? "yellow" : "blue"
                };

                if (alternar)
                    viewModel.PacientesPrincipais.Add(pvm);
                else
                    viewModel.PacientesAssistentes.Add(pvm);

                alternar = !alternar;
            }

            return View(viewModel);
        }
    }
}
