using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClickHealth.Dashboard.Models
{
    [Table("Usuario")]
    public partial class Usuario
    {
        public Usuario()
        {
            Cuidadores = new HashSet<Cuidador>();
            LogsAuditoria = new HashSet<LogAuditoria>();
            Pacientes = new HashSet<Paciente>();
            SessoesUsuario = new HashSet<SessaoUsuario>();
            TentativasLogin = new HashSet<TentativaLogin>();
        }

        [Key]
        [Column("id_usuario")]
        public long IdUsuario { get; set; }

        [Column("email")]
        public string Email { get; set; } = null!;

        [Column("senha_hash")]
        public string SenhaHash { get; set; } = null!;

        [Column("estado")]
        public string? Estado { get; set; }

        [Column("created_at", TypeName = "DATETIME")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at", TypeName = "DATETIME")]
        public DateTime? UpdatedAt { get; set; }

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Cuidador> Cuidadores { get; set; }

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<LogAuditoria> LogsAuditoria { get; set; }

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Paciente> Pacientes { get; set; }

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<SessaoUsuario> SessoesUsuario { get; set; }

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<TentativaLogin> TentativasLogin { get; set; }
    }
}
