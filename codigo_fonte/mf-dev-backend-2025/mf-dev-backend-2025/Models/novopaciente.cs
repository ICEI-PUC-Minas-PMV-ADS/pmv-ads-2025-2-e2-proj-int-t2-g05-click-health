using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mf_dev_backend_2025.Models
{
    [Table("novospacientes")]
    public class novopaciente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Campo Obrigatorio")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatorio")]
        public string email { get; set; }
        
        [Required(ErrorMessage = "Campo Obrigatorio com 9 caracteres+caractere especial+letra maiscula")]
        [DataType(DataType.Password)]
        public string senha { get; set; }
        // NOVO: caminho relativo do arquivo salvo em wwwroot/uploads
        public string? FotoPath { get; set; }
    }
}
