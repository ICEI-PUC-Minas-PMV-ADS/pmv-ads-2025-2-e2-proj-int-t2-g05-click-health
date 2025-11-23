using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClickHealth.Dashboard.Models
{
    public class Notificacao
    {
        [Key]
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;
        public string Mensagem { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public DateTime DataHora { get; set; } = DateTime.Now;
        public bool Lida { get; set; } = false;

        
        [Column("id_paciente")]
        public int? IdPaciente { get; set; }

        
        [ForeignKey("IdPaciente")]
        [InverseProperty("Notificacoes")]
        public virtual Paciente? Paciente { get; set; }
    }
}
