using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClickHealth.Dashboard.Models
{
    [Table("Dispositivo")]
    public partial class Dispositivo
    {
        [Key]
        [Column("id_dispositivo")]
        public int IdDispositivo { get; set; }

        [Column("tipo_dispositivo")]
        public string TipoDispositivo { get; set; } = null!;

        [Column("identificacao")]
        public string? Identificacao { get; set; }

        [Column("status_conexao")]
        public bool? StatusConexao { get; set; }

        [InverseProperty("IdDispositivoNavigation")]
        public virtual ICollection<MonitoramentoSaude> MonitoramentosSaude { get; set; } = new HashSet<MonitoramentoSaude>();
    }
}
