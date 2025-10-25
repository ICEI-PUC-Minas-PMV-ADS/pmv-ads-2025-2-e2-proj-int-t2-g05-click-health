using Microsoft.AspNetCore.Mvc;
using ClickHealth.Dashboard.Models;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks; // Task não é usado nos posts, mas mantido

namespace ClickHealth.Dashboard.Controllers
{
    public class PacienteController : Controller
    {
        private readonly ClickHealthContext _context;

        public PacienteController(ClickHealthContext context)
        {
            _context = context;
        }

        // GET: Passo 1
        [HttpGet]
        public IActionResult CadastroPasso1()
        {
            return View();
        }

        // POST: Passo 1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CadastroPasso1(Paciente paciente)
        {
            var usuarioLogadoId = 1; // Simulação de usuário logado

            // --- INÍCIO DA CORREÇÃO ---
            // Precisamos garantir que o usuário simulado (ID 1) exista no banco 
            // de dados (que está vazio após a recriação).
            var usuarioSimulado = _context.Usuarios.Find((long)usuarioLogadoId);
            
            if (usuarioSimulado == null)
            {
                // Usuário não existe, vamos criá-lo (semear o banco)
                usuarioSimulado = new Usuario
                {
                    // O ID será 1 (autoincremento) se o banco estiver vazio.
                    // Precisamos preencher os campos obrigatórios (NOT NULL).
                    Email = "usuario.simulado@clickhealth.com",
                    SenhaHash = "hash_simulado_requerido_pelo_banco"
                };
                _context.Usuarios.Add(usuarioSimulado);
                _context.SaveChanges(); 
                // Agora o usuário com ID 1 (ou o ID gerado) existe.
            }
            
            // Atribui o ID real do usuário simulado (que será 1 no banco vazio)
            paciente.IdUsuario = usuarioSimulado.IdUsuario;
            // --- FIM DA CORREÇÃO ---


            // Remove validações de propriedades de navegação
            ModelState.Remove(nameof(paciente.IdUsuarioNavigation));
            ModelState.Remove(nameof(paciente.HistoricoMedico));
            ModelState.Remove(nameof(paciente.Alertas));
            ModelState.Remove(nameof(paciente.Medicacoes));
            ModelState.Remove(nameof(paciente.MonitoramentosSaude));

            if (ModelState.IsValid)
            {
                TempData["PacientePasso1"] = JsonSerializer.Serialize(paciente);
                return RedirectToAction(nameof(CadastroPasso2));
            }

            return View(paciente);
        }

        // GET: Passo 2
        [HttpGet]
        public IActionResult CadastroPasso2()
        {
            if (TempData["PacientePasso1"] == null)
                return RedirectToAction(nameof(CadastroPasso1));

            TempData.Keep("PacientePasso1");
            return View();
        }

        // POST: Passo 2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CadastroPasso2(List<string> emailParaConvidar)
        {
            if (TempData["PacientePasso1"] == null)
                return RedirectToAction(nameof(CadastroPasso1));

            var pacienteJson = TempData["PacientePasso1"] as string;
            if (string.IsNullOrEmpty(pacienteJson))
                return RedirectToAction(nameof(CadastroPasso1));

            var paciente = JsonSerializer.Deserialize<Paciente>(pacienteJson)!;

            _context.Pacientes.Add(paciente);
            _context.SaveChanges(); // Salva o paciente (AGORA VAI FUNCIONAR)

            if (emailParaConvidar != null && emailParaConvidar.Any())
            {
                foreach (var email in emailParaConvidar.Where(e => !string.IsNullOrEmpty(e)))
                {
                    var cuidador = new Cuidador
                    {
                        IdUsuario = paciente.IdUsuario,
                        Tipo = "apoio",
                        Nome = email.Split('@').FirstOrDefault() ?? email
                    };
                    _context.Cuidadores.Add(cuidador);
                }
                _context.SaveChanges(); // Salva os novos cuidadores
            }

            TempData.Remove("PacientePasso1");
            return RedirectToAction("Index", "Home");
        }

        // GET: Lista de pacientes
        [HttpGet]
        public IActionResult Index()
        {
            var pacientes = _context.Pacientes.ToList();
            return View(pacientes);
        }

        // GET: Editar paciente
        [HttpGet]
        public IActionResult Editar(long id)
        {
            var paciente = _context.Pacientes.FirstOrDefault(p => p.IdPaciente == id);
            if (paciente == null)
                return NotFound();

            return View(paciente);
        }

        // POST: Editar paciente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(long id, Paciente pacienteAtualizado)
        {
            var paciente = _context.Pacientes.FirstOrDefault(p => p.IdPaciente == id);
            if (paciente == null)
                return NotFound();

            paciente.DadosPessoais = pacienteAtualizado.DadosPessoais;
            paciente.CondicoesMedicas = pacienteAtualizado.CondicoesMedicas;
            paciente.DataNascimento = pacienteAtualizado.DataNascimento;
            paciente.Telefone = pacienteAtualizado.Telefone;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        
        // GET: Excluir (Confirmação)
        [HttpGet]
        public IActionResult Excluir(long id)
        {
            var paciente = _context.Pacientes.FirstOrDefault(p => p.IdPaciente == id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // POST: Excluir (Ação)
        [HttpPost, ActionName("ExcluirConfirmado")]
        [ValidateAntiForgeryToken]
        // --- INÍCIO DA CORREÇÃO ---
        // O parâmetro foi alterado de (long id) para (long IdPaciente)
        // para corresponder ao name="IdPaciente" enviado pelo formulário.
        public IActionResult ExcluirConfirmado(long IdPaciente)
        {
            // A busca agora usa o parâmetro correto.
            var paciente = _context.Pacientes.Find(IdPaciente);
            // --- FIM DA CORREÇÃO ---
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}