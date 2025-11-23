namespace ClickHealth.Dashboard.Models.ViewModels
{
    public class UsuariosCreateViewModel
    {
        // Dados do Paciente
        public string DadosPessoais { get; set; }
        public DateTime DataNascimento { get; set; }

        // Dados do Usuario
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }

        // Foto
        public IFormFile Foto { get; set; }
    }
}
