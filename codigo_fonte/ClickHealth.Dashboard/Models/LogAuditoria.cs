using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClickHealth.Dashboard.Models
{
    [Table("LogAuditoria")]
    public partial class LogAuditoria
    {
        [Key]
        [Column("id_log")]
        public long IdLog { get; set; }

        [Column("id_usuario")]
        public long IdUsuario { get; set; }

        [Column("acao")]
        public string Acao { get; set; } = null!;

        [Column("data_hora", TypeName = "DATETIME")]
        public DateTime? DataHora { get; set; } = null!;

        [Column("descricao")]
        public string? Descricao { get; set; }

        [ForeignKey("IdUsuario")]
        [InverseProperty("LogsAuditoria")]
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
