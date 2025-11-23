using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClickHealth.Dashboard.Models
{
    [Table("RegistroClinico")]
    public class RegistroClinico
    {
        [Key]
        [Column("id_registro")]
        public int Id { get; set; }

        [Required]
        [Column("id_paciente")]
        public int IdPaciente { get; set; }

        // Navegação para Paciente (opcional, mas virtual é recomendado)
        [ForeignKey("IdPaciente")]
        [InverseProperty("RegistrosClinicos")]
        public virtual Paciente? Paciente { get; set; }

        [Column("data_registro")]
        public DateTime DataRegistro { get; set; } = DateTime.Now;

        [Column("resumo")]
        [Display(Name = "Resumo do Atendimento / Registro")]
        public string? Resumo { get; set; }   // pode ser nulo

        [Column("observacoes")]
        [Display(Name = "Observações")]
        public string? Observacoes { get; set; }   // pode ser nulo

        // JSON dos exames — pode ser nulo
        [Column("exames_json")]
        public string? ExamesJson { get; set; } = "[]";

        // Caminho do arquivo — opcional
        [Column("arquivo_path")]
        public string? ArquivoPath { get; set; }
    }
}
