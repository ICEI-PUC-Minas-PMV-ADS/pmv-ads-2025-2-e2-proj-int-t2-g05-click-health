using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClickHealth.Dashboard.Models
{
    [Table("Paciente")]
    public partial class Paciente
    {
        public Paciente()
        {
            Alertas = new HashSet<Alerta>();
            Medicacoes = new HashSet<Medicacao>();
            MonitoramentosSaude = new HashSet<MonitoramentoSaude>();
            RegistrosClinicos = new HashSet<RegistroClinico>();
            AgendamentosMedicacao = new HashSet<AgendamentoMedicacao>(); // ← OK
            Notificacoes = new HashSet<Notificacao>();
        }

        [Key]
        [Column("id_paciente")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPaciente { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        // Campos do banco
        [Column("condicoes_medicas")]
        public string? CondicoesMedicas { get; set; }

        [Column("dados_pessoais")]
        public string? DadosPessoais { get; set; }

        [Column("data_nascimento")]
        public DateTime? DataNascimento { get; set; }

        [Column("telefone")]
        public string? Telefone { get; set; }

        // Campos que NÃO vão para o banco
        [NotMapped] public string? Email { get; set; }
        [NotMapped] public string? Senha { get; set; }
        [NotMapped] public string? FotoPath { get; set; }

        // ==================== RELACIONAMENTOS ====================

        // Usuário dono do paciente
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty("Pacientes")]
        public virtual Usuario? IdUsuarioNavigation { get; set; }

        // Histórico médico (1:1)
        [InverseProperty("IdPacienteNavigation")]
        public virtual HistoricoMedico? HistoricoMedico { get; set; }

        // Coleções
        [InverseProperty("IdPacienteNavigation")]
        public virtual ICollection<Alerta> Alertas { get; set; }

        [InverseProperty("Paciente")]
        public virtual ICollection<Medicacao> Medicacoes { get; set; }

        [InverseProperty("IdPacienteNavigation")]
        public virtual ICollection<MonitoramentoSaude> MonitoramentosSaude { get; set; }

        [InverseProperty("Paciente")]
        public virtual ICollection<RegistroClinico> RegistrosClinicos { get; set; }

        // Agendamentos de medicação do paciente
        [InverseProperty("Paciente")]
        public virtual ICollection<AgendamentoMedicacao> AgendamentosMedicacao { get; set; }

        // Notificações (caso tenha)
        public virtual ICollection<Notificacao> Notificacoes { get; set; }
    }
}