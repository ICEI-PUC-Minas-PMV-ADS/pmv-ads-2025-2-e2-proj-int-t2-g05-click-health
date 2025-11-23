using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClickHealth.Dashboard.Models
{
    [Table("AgendamentoMedicacao")]
    public class AgendamentoMedicacao
    {
        [Key]
        public int Id { get; set; }

        // Chaves estrangeiras
        [Required(ErrorMessage = "Selecione um paciente")]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "Selecione a medicação")]
        public int IdMedicacao { get; set; }

        // Dados do agendamento
        [Required(ErrorMessage = "Informe a data e hora")]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "Informe a dose")]
        [StringLength(100)]
        public string Dose { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Observacoes { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Pendente"; // Pendente, Agendado, Concluído, Cancelado...

        public DateTime? DataCadastro { get; set; } = DateTime.Now;

        // =============================================
        // RELACIONAMENTOS (NAVEGAÇÃO)
        // =============================================

        [ForeignKey(nameof(IdPaciente))]
        [InverseProperty("AgendamentosMedicacao")]
        public virtual Paciente Paciente { get; set; } = null!;

        [ForeignKey(nameof(IdMedicacao))]
        [InverseProperty("Agendamentos")]
        public virtual Medicacao Medicacao { get; set; } = null!;
    }
}