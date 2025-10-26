using System;
using System.ComponentModel.DataAnnotations;

namespace ClickHealth.Models
{
    public class Agendamento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do paciente é obrigatório")]
        public string NomePaciente { get; set; }

        [Required(ErrorMessage = "Informe a data do agendamento")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Informe o horário")]
        [DataType(DataType.Time)]
        public TimeSpan Hora { get; set; }

        [StringLength(200)]
        public string Observacao { get; set; }
    }
}
