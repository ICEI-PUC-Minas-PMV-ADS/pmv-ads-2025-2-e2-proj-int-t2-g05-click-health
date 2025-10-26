using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClickHealth.Models
{
    public class MedicamentoTomado
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do paciente")]
        public string NomePaciente { get; set; }

        [Required(ErrorMessage = "Selecione o medicamento")]
        public int MedicamentoId { get; set; }

        [ForeignKey("MedicamentoId")]
        public Medicamento Medicamento { get; set; }

        [Required(ErrorMessage = "Informe a data")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Informe o horário")]
        [DataType(DataType.Time)]
        public TimeSpan Hora { get; set; }

        [StringLength(200)]
        public string Observacao { get; set; }
    }
}
