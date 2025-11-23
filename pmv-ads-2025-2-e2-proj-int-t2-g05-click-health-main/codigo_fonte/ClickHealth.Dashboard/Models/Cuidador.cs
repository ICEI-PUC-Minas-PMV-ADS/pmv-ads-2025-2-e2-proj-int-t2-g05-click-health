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

        // 🔥 CORRIGIDO: agora int (compatível com Usuario.IdUsuario)
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Column("tipo")]
        public string Tipo { get; set; } = "Assistente";

        [ForeignKey("IdUsuario")]
        [InverseProperty("Cuidadores")]
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
