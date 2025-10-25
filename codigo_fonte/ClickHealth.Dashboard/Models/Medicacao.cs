using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClickHealth.Dashboard.Models
{
    [Table("Medicacao")]
    public partial class Medicacao
    {
        [Key]
        [Column("id_medicacao")]
        public int IdMedicacao { get; set; }

        [Column("id_paciente")]
        public long IdPaciente { get; set; }

        [Column("nome")]
        public string Nome { get; set; } = null!;

        [Column("dosagem")]
        public string Dosagem { get; set; } = null!;

        [Column("frequencia")]
        public string? Frequencia { get; set; }

        [Column("horario_administracao", TypeName = "DATETIME")]
        public DateTime? HorarioAdministracao { get; set; }

        [ForeignKey("IdPaciente")]
        [InverseProperty("Medicacoes")]
        public virtual Paciente IdPacienteNavigation { get; set; } = null!;
    }
}
