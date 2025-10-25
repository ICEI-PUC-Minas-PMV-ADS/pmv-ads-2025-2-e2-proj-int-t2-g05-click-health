using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClickHealth.Dashboard.Models
{
    [Table("HistoricoMedico")]
    public partial class HistoricoMedico
    {
        [Key]
        [Column("id_historico")]
        public long IdHistorico { get; set; }

        [Column("id_paciente")]
        public long IdPaciente { get; set; }

        [Column("diagnosticos_passados")]
        public string? DiagnosticosPassados { get; set; }

        [Column("alergias")]
        public string? Alergias { get; set; }

        [Column("intervencoes")]
        public string? Intervencoes { get; set; }

        [Column("procedimentos")]
        public string? Procedimentos { get; set; }

        [Column("atualizado_em", TypeName = "DATETIME")]
        public DateTime? AtualizadoEm { get; set; }

        [ForeignKey("IdPaciente")]
        [InverseProperty("HistoricoMedico")]
        public virtual Paciente IdPacienteNavigation { get; set; } = null!;
    }
}
