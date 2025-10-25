using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClickHealth.Dashboard.Models
{
    [Table("Alerta")]
    public partial class Alerta
    {
        [Key]
        [Column("id_alerta")]
        public int IdAlerta { get; set; }

        [Column("id_paciente")]
        public long IdPaciente { get; set; }

        [Column("mensagem")]
        public string Mensagem { get; set; } = null!;

        [Column("data_hora", TypeName = "DATETIME")]
        public DateTime DataHora { get; set; }

        [ForeignKey("IdPaciente")]
        [InverseProperty("Alertas")]
        public virtual Paciente IdPacienteNavigation { get; set; } = null!;
    }
}
