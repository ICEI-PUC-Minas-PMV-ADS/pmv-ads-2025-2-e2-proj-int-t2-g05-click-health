using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClickHealth.Dashboard.Models
{
    [Table("TentativaLogin")]
    public partial class TentativaLogin
    {
        [Key]
        [Column("id_tentativa")]
        public int IdTentativa { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("data_hora", TypeName = "DATETIME")]
        public DateTime DataHora { get; set; }

        [Column("resultado")]
        public string? Resultado { get; set; }

        [Column("ip_origem")]
        public string? IpOrigem { get; set; }

        [ForeignKey("IdUsuario")]
        [InverseProperty("TentativasLogin")]
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
