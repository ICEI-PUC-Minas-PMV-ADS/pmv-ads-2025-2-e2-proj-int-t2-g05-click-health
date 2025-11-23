using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClickHealth.Dashboard.Models
{
    // Mantém o nome da tabela antiga no banco (com ç)
    [Table("Medicacoes")]
    public class Medicamento
    {
        // Chave primária – nome da coluna no banco continua "id_medicacao"
        [Key]
        [Column("id_medicacao")]
        public int IdMedicamento { get; set; }       

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
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [Column("data_inicio")]
        public DateTime? DataInicio { get; set; }

        [Column("data_fim")]
        public DateTime? DataFim { get; set; }

        // ==========================================
        // RELACIONAMENTO COM PACIENTE
        // ==========================================
        [Required(ErrorMessage = "O paciente é obrigatório")]
        [Column("id_paciente")]
        public int IdPaciente { get; set; }

        [ForeignKey(nameof(IdPaciente))]
        [InverseProperty("Medicamentos")]  // ← agora é Medicamentos (com t)
        public virtual Paciente Paciente { get; set; } = null!;

        // ==========================================
        // RELACIONAMENTO COM AGENDAMENTOS (1:N)
        // ==========================================
        [InverseProperty("Medicamento")]   // ← aponta para a propriedade Medicamento na AgendamentoMedicacao
        public virtual ICollection<AgendamentoMedicacao> Agendamentos { get; set; } =
    new List<AgendamentoMedicacao>();
    }
}