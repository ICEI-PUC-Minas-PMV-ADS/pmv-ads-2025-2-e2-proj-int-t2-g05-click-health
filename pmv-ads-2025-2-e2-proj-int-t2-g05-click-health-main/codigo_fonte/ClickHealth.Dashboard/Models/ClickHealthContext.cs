using Microsoft.EntityFrameworkCore;
using ClickHealth.Dashboard.Models;

namespace ClickHealth.Dashboard.Models
{
    public partial class ClickHealthContext : DbContext
    {
        public ClickHealthContext(DbContextOptions<ClickHealthContext> options)
            : base(options)
        {
        }

        // ===============================
        // DbSets – TODAS AS TABELAS
        // ===============================
        public virtual DbSet<Alerta> Alertas { get; set; } = null!;
        public virtual DbSet<Cuidador> Cuidadores { get; set; } = null!;
        public virtual DbSet<Dispositivo> Dispositivos { get; set; } = null!;
        public virtual DbSet<HistoricoMedico> HistoricosMedicos { get; set; } = null!;
        public virtual DbSet<LogAuditoria> LogsAuditoria { get; set; } = null!;
        public virtual DbSet<Medicacao> Medicacoes { get; set; } = null!;
        public virtual DbSet<MonitoramentoSaude> MonitoramentosSaude { get; set; } = null!;
        public virtual DbSet<Paciente> Pacientes { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<SessaoUsuario> SessoesUsuario { get; set; } = null!;
        public virtual DbSet<TentativaLogin> TentativasLogin { get; set; } = null!;
        public virtual DbSet<Notificacao> Notificacoes { get; set; } = null!;
        public virtual DbSet<RegistroClinico> RegistrosClinicos { get; set; } = null!;

        // NOVA TABELA DE AGENDAMENTO
        public virtual DbSet<AgendamentoMedicacao> AgendamentoMedicacao { get; set; } = null!;

        // ===============================
        // CONFIGURAÇÃO DO MODELO
        // ===============================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // === PACIENTE ===
            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasKey(e => e.IdPaciente);
                entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");
                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.HasOne(d => d.IdUsuarioNavigation)
                      .WithMany(p => p.Pacientes)
                      .HasForeignKey(d => d.IdUsuario)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // === MEDICAÇÃO ===
            modelBuilder.Entity<Medicacao>(entity =>
            {
                entity.HasKey(e => e.IdMedicacao);
                entity.Property(e => e.IdMedicacao).HasColumnName("id_medicacao");
                entity.Property(e => e.IdPaciente).HasColumnName("id_paciente").IsRequired();

                entity.HasOne(d => d.Paciente)
                      .WithMany(p => p.Medicacoes)
                      .HasForeignKey(d => d.IdPaciente)
                      .OnDelete(DeleteBehavior.Cascade); // se apagar paciente, apaga medicações
            });

            // === AGENDAMENTO DE MEDICAÇÃO ===
            modelBuilder.Entity<AgendamentoMedicacao>(entity =>
            {
                entity.ToTable("AgendamentoMedicacao");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.IdPaciente).IsRequired();
                entity.Property(e => e.IdMedicacao).IsRequired();

                // Paciente → Agendamentos
                entity.HasOne(d => d.Paciente)
                      .WithMany(p => p.AgendamentosMedicacao)
                      .HasForeignKey(d => d.IdPaciente)
                      .OnDelete(DeleteBehavior.Cascade);

                // Medicação → Agendamentos
                entity.HasOne(d => d.Medicacao)
                      .WithMany(m => m.Agendamentos)
                      .HasForeignKey(d => d.IdMedicacao)
                      .OnDelete(DeleteBehavior.Restrict); // não permite apagar medicação que tem agendamento
            });

            // === ALERTA ===
            modelBuilder.Entity<Alerta>(entity =>
            {
                entity.HasKey(e => e.IdAlerta);
                entity.HasOne(d => d.IdPacienteNavigation)
                      .WithMany(p => p.Alertas)
                      .HasForeignKey(d => d.IdPaciente)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // === HISTÓRICO MÉDICO (1:1) ===
            modelBuilder.Entity<HistoricoMedico>(entity =>
            {
                entity.HasKey(e => e.IdHistorico);
                entity.HasOne(d => d.IdPacienteNavigation)
                      .WithOne(p => p.HistoricoMedico)
                      .HasForeignKey<HistoricoMedico>(d => d.IdPaciente)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // === MONITORAMENTO DE SAÚDE ===
            modelBuilder.Entity<MonitoramentoSaude>(entity =>
            {
                entity.HasKey(e => e.IdMonitoramento);
                entity.HasOne(d => d.IdPacienteNavigation)
                      .WithMany(p => p.MonitoramentosSaude)
                      .HasForeignKey(d => d.IdPaciente)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // === NOTIFICAÇÃO ===
            modelBuilder.Entity<Notificacao>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Paciente)
                      .WithMany(p => p.Notificacoes)
                      .HasForeignKey(e => e.IdPaciente)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // === REGISTRO CLÍNICO ===
            modelBuilder.Entity<RegistroClinico>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Paciente)
                      .WithMany(p => p.RegistrosClinicos)
                      .HasForeignKey(e => e.IdPaciente)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // === USUÁRIO ===
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // === DEMAIS ENTIDADES (mantidas como estavam) ===
            modelBuilder.Entity<Cuidador>(e => e.HasKey(x => x.IdCuidador));
            modelBuilder.Entity<Dispositivo>(e => e.HasKey(x => x.IdDispositivo));
            modelBuilder.Entity<LogAuditoria>(e => e.HasKey(x => x.IdLog));
            modelBuilder.Entity<SessaoUsuario>(e => e.HasKey(x => x.IdSessao));
            modelBuilder.Entity<TentativaLogin>(e => e.HasKey(x => x.IdTentativa));

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}