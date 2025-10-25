using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClickHealth.Dashboard.Models
{
    [Table("SessaoUsuario")]
    public partial class SessaoUsuario
    {
        [Key]
        [Column("id_sessao")]
        public int IdSessao { get; set; }

        [Column("id_usuario")]
        public long IdUsuario { get; set; }

        [Column("token_sessao")]
        public string TokenSessao { get; set; } = null!;

        [Column("data_inicio", TypeName = "DATETIME")]
        public DateTime DataInicio { get; set; }

        [Column("data_fim", TypeName = "DATETIME")]
        public DateTime? DataFim { get; set; }

        [ForeignKey("IdUsuario")]
        [InverseProperty("SessoesUsuario")]
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
