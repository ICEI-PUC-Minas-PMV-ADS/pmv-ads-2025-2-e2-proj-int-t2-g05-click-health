using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ClickHealth.Dashboard.Models;

namespace ClickHealth.Dashboard.Controllers
{
    public class AccountController : Controller
    {
        private readonly ClickHealthContext _context;

        public AccountController(ClickHealthContext context)
        {
            _context = context;
        }

        // ========================================
        // LOGIN
        // ========================================
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // *** LOGIN CORRETO USANDO USUARIO ***
            var usuario = await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u =>
                    u.Email == model.Email &&
                    u.SenhaHash == model.Password);

            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
                return View(model);
            }

            // Pega o paciente vinculado
            var paciente = await _context.Pacientes
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.IdUsuario == usuario.IdUsuario);

            if (paciente == null)
            {
                ModelState.AddModelError("", "Usuário não está vinculado a um paciente.");
                return View(model);
            }

            // Salva sessão
            HttpContext.Session.SetInt32("UserId", (int)usuario.IdUsuario);
            HttpContext.Session.SetString("UserEmail", usuario.Email ?? "");
            HttpContext.Session.SetString("UserName", paciente.DadosPessoais ?? "");

            return RedirectToAction("Index", "Home");
        }

        // ========================================
        // MINHA CONTA
        // ========================================
        [HttpGet]
        public IActionResult MinhaConta()
        {
            var idUsuario = HttpContext.Session.GetInt32("UserId");
            if (idUsuario == null)
                return RedirectToAction(nameof(Login));

            var paciente = _context.Pacientes.FirstOrDefault(p => p.IdUsuario == idUsuario.Value);
            if (paciente == null)
                return RedirectToAction(nameof(Login));

            return RedirectToAction("Details", "Usuarios", new { id = paciente.IdPaciente });
        }

        // ========================================
        // REGISTER → redireciona para criar usuário
        // ========================================
        [HttpGet]
        public IActionResult Register()
        {
            return RedirectToAction("Create", "Usuarios");
        }

        // ESTA ACTION NÃO DEVE MAIS EXISTIR
        // pois o form de registro usa UsuarioController
    }
}
