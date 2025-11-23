// NOVO CÓDIGO PARA O FeedViewModel.cs
using System.Collections.Generic;
using ClickHealth.Dashboard.Models;
using System;

namespace ClickHealth.Dashboard.Models.ViewModels
{
    public class FeedViewModel
    {
        public IEnumerable<AgendamentoMedicacao> AgendamentosRecentes { get; set; } // CORRIGIDO
        public IEnumerable<Medicacao> MedicamentosAtivos { get; set; }
        public IEnumerable<Alerta> Alertas { get; set; }
        public IEnumerable<HistoricoMedico> HistoricoMedicoRecente { get; set; }
        public string MensagemStatus { get; set; }
    }
}
