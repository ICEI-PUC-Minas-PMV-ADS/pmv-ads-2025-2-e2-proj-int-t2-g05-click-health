using ClickHealth.Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ClickHealth.Dashboard.Controllers
{
    // Controller da página inicial (Dashboard)
    public class HomeController : Controller
    {
        private readonly ClickHealthContext _context;

        public HomeController(ClickHealthContext context)
        {
            _context = context;
        }

        // Action principal que exibe o Dashboard
        public async Task<IActionResult> Index()
        {
            // Simula o usuário logado
            // CORREÇÃO: Alterado para 'long' (L) para corresponder ao tipo de IdUsuario
            var usuarioLogadoId = 1L; 
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == usuarioLogadoId);

            // Se o usuário simulado não existir (primeira execução após recriar o banco),
            // cria ele para evitar erros de Foreign Key.
            if (usuario == null)
            {
                usuario = new Usuario { Email = "admin@clickhealth.com", SenhaHash = "admin_hash_temp" };
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                // Atribui o ID 'long' que foi gerado pelo banco
                usuarioLogadoId = usuario.IdUsuario; 
            }

            // Busca os últimos 5 pacientes cadastrados no banco
            var pacientesDoBanco = await _context.Pacientes
                                           .OrderByDescending(p => p.IdPaciente)
                                           .Take(5)
                                           .ToListAsync();

            // Prepara o ViewModel para enviar para a View
            var viewModel = new DashboardViewModel();
            viewModel.NomeUsuarioLogado = usuario?.Email ?? "Usuário"; // Pega o email ou usa "Usuário"

            // Log para verificar quantos pacientes foram encontrados
            System.Diagnostics.Debug.WriteLine($"Número de pacientes encontrados: {pacientesDoBanco.Count}");

            // Separa os pacientes encontrados nas listas do ViewModel (lógica alternada)
            bool alternar = true;
            foreach (var paciente in pacientesDoBanco)
            {
                var pacienteVM = new PacienteViewModel
                {
                    // ADICIONADO: Mapeia o ID do paciente para o ViewModel
                    IdPaciente = paciente.IdPaciente, 
                    NomePaciente = paciente.DadosPessoais ?? $"Paciente ID: {paciente.IdPaciente}",
                    ProximaAcao = paciente.CondicoesMedicas ?? "Nenhuma condição registrada",
                    StatusIndicador = alternar ? "yellow" : "blue"
                };

                if (alternar)
                {
                    viewModel.PacientesPrincipais.Add(pacienteVM);
                }
                else
                {
                    viewModel.PacientesAssistentes.Add(pacienteVM);
                }
                alternar = !alternar;
            }

            // Envia o ViewModel para a View Index.cshtml
            return View(viewModel);
        }

        // Outras actions como Privacy, Error podem existir
        // ...

    }
}