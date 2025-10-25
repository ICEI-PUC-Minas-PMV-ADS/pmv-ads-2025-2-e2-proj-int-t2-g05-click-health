using System;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace ClickHealth.Dashboard.Models
{
    // Contexto principal do banco
    public partial class ClickHealthContext : DbContext
    {
        public ClickHealthContext(DbContextOptions<ClickHealthContext> options)
            : base(options)
        {
        }

        // DbSets
        public virtual DbSet<Alerta> Alertas { get; set; }
        public virtual DbSet<Cuidador> Cuidadores { get; set; }
        public virtual DbSet<Dispositivo> Dispositivos { get; set; }
        public virtual DbSet<HistoricoMedico> HistoricosMedicos { get; set; }
        public virtual DbSet<LogAuditoria> LogsAuditoria { get; set; }
        public virtual DbSet<Medicacao> Medicacoes { get; set; }
        public virtual DbSet<MonitoramentoSaude> MonitoramentosSaude { get; set; }
        public virtual DbSet<Paciente> Pacientes { get; set; }
        public virtual DbSet<SessaoUsuario> SessoesUsuario { get; set; }
        public virtual DbSet<TentativaLogin> TentativasLogin { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alerta>(entity =>
            {
                entity.HasKey(e => e.IdAlerta);
                entity.HasOne(d => d.IdPacienteNavigation)
                      .WithMany(p => p.Alertas)
                      .HasForeignKey(d => d.IdPaciente)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Cuidador>(entity =>
            {
                entity.HasKey(e => e.IdCuidador);
                entity.HasOne(d => d.IdUsuarioNavigation)
                      .WithMany(p => p.Cuidadores)
                      .HasForeignKey(d => d.IdUsuario)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Dispositivo>(entity =>
            {
                entity.HasKey(e => e.IdDispositivo);
                entity.Property(e => e.StatusConexao).HasDefaultValue(true);
            });

            modelBuilder.Entity<HistoricoMedico>(entity =>
            {
                entity.HasKey(e => e.IdHistorico);
                entity.Property(e => e.AtualizadoEm).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.HasOne(d => d.IdPacienteNavigation)
                      .WithOne(p => p.HistoricoMedico)
                      .HasForeignKey<HistoricoMedico>(d => d.IdPaciente)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<LogAuditoria>(entity =>
            {
                entity.HasKey(e => e.IdLog);
                entity.HasOne(d => d.IdUsuarioNavigation)
                      .WithMany(p => p.LogsAuditoria)
                      .HasForeignKey(d => d.IdUsuario)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Medicacao>(entity =>
            {
                entity.HasKey(e => e.IdMedicacao);
                entity.HasOne(d => d.IdPacienteNavigation)
                      .WithMany(p => p.Medicacoes)
                      .HasForeignKey(d => d.IdPaciente)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MonitoramentoSaude>(entity =>
            {
                entity.HasKey(e => e.IdMonitoramento);
                entity.HasOne(d => d.IdPacienteNavigation)
                      .WithMany(p => p.MonitoramentosSaude)
                      .HasForeignKey(d => d.IdPaciente)
                      .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.IdDispositivoNavigation)
                      .WithMany(p => p.MonitoramentosSaude)
                      .HasForeignKey(d => d.IdDispositivo);
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasKey(e => e.IdPaciente);
                entity.HasOne(d => d.IdUsuarioNavigation)
                      .WithMany(p => p.Pacientes)
                      .HasForeignKey(d => d.IdUsuario)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SessaoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdSessao);
                entity.HasOne(d => d.IdUsuarioNavigation)
                      .WithMany(p => p.SessoesUsuario)
                      .HasForeignKey(d => d.IdUsuario)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TentativaLogin>(entity =>
            {
                entity.HasKey(e => e.IdTentativa);
                entity.HasOne(d => d.IdUsuarioNavigation)
                      .WithMany(p => p.TentativasLogin)
                      .HasForeignKey(d => d.IdUsuario)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
