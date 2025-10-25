using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClickHealth.Dashboard.Models
{
    [Table("MonitoramentoSaude")]
    public partial class MonitoramentoSaude
    {
        [Key]
        [Column("id_monitoramento")]
        public int IdMonitoramento { get; set; }

        [Column("id_paciente")]
        public long IdPaciente { get; set; }

        [Column("frequencia_cardiaca")]
        public int? FrequenciaCardiaca { get; set; }

        [Column("pressao_arterial")]
        public string? PressaoArterial { get; set; }

        [Column("temperatura", TypeName = "DECIMAL(4,1)")]
        public decimal? Temperatura { get; set; }

        [Column("glicose", TypeName = "DECIMAL(5,2)")]
        public decimal? Glicose { get; set; }

        [Column("data_hora", TypeName = "DATETIME")]
        public DateTime DataHora { get; set; }

        [Column("id_dispositivo")]
        public int? IdDispositivo { get; set; }

        [ForeignKey("IdDispositivo")]
        [InverseProperty("MonitoramentosSaude")]
        public virtual Dispositivo? IdDispositivoNavigation { get; set; }

        [ForeignKey("IdPaciente")]
        [InverseProperty("MonitoramentosSaude")]
        public virtual Paciente IdPacienteNavigation { get; set; } = null!;
    }
}
