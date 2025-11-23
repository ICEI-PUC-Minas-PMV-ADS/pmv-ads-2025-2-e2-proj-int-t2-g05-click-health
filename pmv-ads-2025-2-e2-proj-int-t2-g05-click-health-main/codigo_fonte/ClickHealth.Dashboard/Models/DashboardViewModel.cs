using System.Collections.Generic;

namespace ClickHealth.Dashboard.Models
{
    public class PacienteViewModel
    {
        public int IdPaciente { get; set; }
        public string? NomePaciente { get; set; }
        public string? ProximaAcao { get; set; }
        public string? StatusIndicador { get; set; } // "yellow" ou "blue"
    }

    public class CuidadorViewModel
    {
        public string Email { get; set; } = null!;
        public string Tipo { get; set; } = "Assistente";
    }

    public class DashboardViewModel
    {
        public string? NomeUsuarioLogado { get; set; }
        
        public List<PacienteViewModel> PacientesPrincipais { get; set; }
        public List<PacienteViewModel> PacientesAssistentes { get; set; }

        public List<CuidadorViewModel> Cuidadores { get; set; } 

        public DashboardViewModel()
        {
            PacientesPrincipais = new List<PacienteViewModel>();
            PacientesAssistentes = new List<PacienteViewModel>();
            Cuidadores = new List<CuidadorViewModel>(); 
        }
    }
}