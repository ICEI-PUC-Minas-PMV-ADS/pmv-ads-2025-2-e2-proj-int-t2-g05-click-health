using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClickHealth.Dashboard.Models
{
    [Table("Cuidador")]
    public partial class Cuidador
    {
        [Key]
        [Column("id_cuidador")]
        public int IdCuidador { get; set; }

        [Column("id_usuario")]
        public long IdUsuario { get; set; }

        [Column("nome")]
        public string Nome { get; set; } = null!;

        [Column("tipo")]
        public string Tipo { get; set; } = "Assistente"; // Valor padrão

        [ForeignKey("IdUsuario")]
        [InverseProperty("Cuidadores")]
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
