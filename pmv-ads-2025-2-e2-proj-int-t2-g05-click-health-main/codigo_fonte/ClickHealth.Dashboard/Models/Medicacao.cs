using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClickHealth.Dashboard.Models
{
    [Table("Medicacao")]
    public class Medicacao
    {
        [Key]
        [Column("id_medicacao")]
        public int IdMedicacao { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(200)]
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(100)]
        [Column("dosagem")]
        public string? Dosagem { get; set; }

        [StringLength(500)]
        [Column("instrucoes")]
        public string? Instrucoes { get; set; }

        [StringLength(200)]
        [Column("principio_ativo")]
        public string? PrincipioAtivo { get; set; }

        [Column("data_cadastro")]
        public DateTime? DataCadastro { get; set; } = DateTime.Now;

        // ==========================================
        // RELACIONAMENTO COM PACIENTE (obrigatório)
        // ==========================================
        [Required(ErrorMessage = "O paciente é obrigatório")]
        [Column("id_paciente")]
        public int IdPaciente { get; set; }

        [ForeignKey(nameof(IdPaciente))]
        [InverseProperty("Medicacoes")]
        public virtual Paciente Paciente { get; set; } = null!;

        // ==========================================
        // RELACIONAMENTO COM AGENDAMENTOS (1:N)
        // ==========================================
        [InverseProperty("Medicacao")]
        public virtual ICollection<AgendamentoMedicacao> Agendamentos { get; set; }
            = new HashSet<AgendamentoMedicacao>();
    }
}