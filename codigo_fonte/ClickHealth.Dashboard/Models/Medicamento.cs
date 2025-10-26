using System.ComponentModel.DataAnnotations;

namespace ClickHealth.Models
{
    public class Medicamento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do medicamento é obrigatório")]
        public string Nome { get; set; }

        [StringLength(200)]
        public string Descricao { get; set; }
    }
}
