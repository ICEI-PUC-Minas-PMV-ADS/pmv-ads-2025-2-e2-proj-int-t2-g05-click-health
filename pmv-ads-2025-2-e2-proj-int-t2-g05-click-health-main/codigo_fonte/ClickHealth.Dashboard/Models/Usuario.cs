using System;
using System.Collections.Generic;

namespace ClickHealth.Dashboard.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }

        public string Email { get; set; } = null!;

        public string SenhaHash { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // 🔥 NOVA PROPRIEDADE — NECESSÁRIA PARA TODAS AS SUAS TELAS
        public string? FotoPath { get; set; }

        // ===============================
        // Relacionamentos (navegação)
        // ===============================

        public virtual ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();

        public virtual ICollection<LogAuditoria> LogsAuditoria { get; set; } = new List<LogAuditoria>();

        public virtual ICollection<SessaoUsuario> SessoesUsuario { get; set; } = new List<SessaoUsuario>();

        public virtual ICollection<TentativaLogin> TentativasLogin { get; set; } = new List<TentativaLogin>();

        public virtual ICollection<Cuidador> Cuidadores { get; set; } = new List<Cuidador>();

        public bool IsAdmin { get; set; } = false;
    }
}
