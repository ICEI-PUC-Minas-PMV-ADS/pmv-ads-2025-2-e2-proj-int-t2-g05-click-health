using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mf_dev_backend_2025.Models
{
    [Table("novospacientes")]
    public class novopaciente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Campo Obrigratorio")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigratorio")]
        public string email { get; set; }

        [Required(ErrorMessage = "Campo Obrigratorio com 9caracteres+caractereespecial+letra maiscula")]
        public string senha { get; set; }

    }
}
