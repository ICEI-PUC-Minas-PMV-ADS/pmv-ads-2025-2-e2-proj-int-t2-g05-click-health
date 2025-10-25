using System;
using System.Collections.Generic;
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
        }

        [Key]
        [Column("id_paciente")]
        public long IdPaciente { get; set; }

        [Column("id_usuario")]
        public long IdUsuario { get; set; }

        [Column("condicoes_medicas")]
        public string? CondicoesMedicas { get; set; }

        [Display(Name = "Nome Completo")]
        [Column("dados_pessoais")]
        public string? DadosPessoais { get; set; }

        
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        [NotMapped]
        public DateTime? DataNascimento { get; set; }

        [Phone(ErrorMessage = "Formato de telefone inválido.")]
        [StringLength(20)]
        [Display(Name = "Telefone")]
        [NotMapped]
        public string? Telefone { get; set; }

        [ForeignKey("IdUsuario")]
        [InverseProperty("Pacientes")]
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

        [InverseProperty("IdPacienteNavigation")]
        public virtual HistoricoMedico? HistoricoMedico { get; set; }

        [InverseProperty("IdPacienteNavigation")]
        public virtual ICollection<Alerta> Alertas { get; set; }

        [InverseProperty("IdPacienteNavigation")]
        public virtual ICollection<Medicacao> Medicacoes { get; set; }

        [InverseProperty("IdPacienteNavigation")]
        public virtual ICollection<MonitoramentoSaude> MonitoramentosSaude { get; set; }
    }
}
