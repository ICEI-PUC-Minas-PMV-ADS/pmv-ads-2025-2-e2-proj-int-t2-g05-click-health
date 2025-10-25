using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClickHealth.Dashboard.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoInicialCompleta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dispositivo",
                columns: table => new
                {
                    id_dispositivo = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    tipo_dispositivo = table.Column<string>(type: "TEXT", nullable: false),
                    identificacao = table.Column<string>(type: "TEXT", nullable: true),
                    status_conexao = table.Column<bool>(type: "INTEGER", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispositivo", x => x.id_dispositivo);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id_usuario = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    senha_hash = table.Column<string>(type: "TEXT", nullable: false),
                    estado = table.Column<string>(type: "TEXT", nullable: true),
                    created_at = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "Cuidador",
                columns: table => new
                {
                    id_cuidador = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_usuario = table.Column<long>(type: "INTEGER", nullable: false),
                    nome = table.Column<string>(type: "TEXT", nullable: false),
                    tipo = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuidador", x => x.id_cuidador);
                    table.ForeignKey(
                        name: "FK_Cuidador_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "LogAuditoria",
                columns: table => new
                {
                    id_log = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_usuario = table.Column<long>(type: "INTEGER", nullable: false),
                    acao = table.Column<string>(type: "TEXT", nullable: false),
                    data_hora = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    descricao = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogAuditoria", x => x.id_log);
                    table.ForeignKey(
                        name: "FK_LogAuditoria_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    id_paciente = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_usuario = table.Column<long>(type: "INTEGER", nullable: false),
                    condicoes_medicas = table.Column<string>(type: "TEXT", nullable: true),
                    dados_pessoais = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.id_paciente);
                    table.ForeignKey(
                        name: "FK_Paciente_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "SessaoUsuario",
                columns: table => new
                {
                    id_sessao = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_usuario = table.Column<long>(type: "INTEGER", nullable: false),
                    token_sessao = table.Column<string>(type: "TEXT", nullable: false),
                    data_inicio = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    data_fim = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessaoUsuario", x => x.id_sessao);
                    table.ForeignKey(
                        name: "FK_SessaoUsuario_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "TentativaLogin",
                columns: table => new
                {
                    id_tentativa = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_usuario = table.Column<long>(type: "INTEGER", nullable: false),
                    data_hora = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    resultado = table.Column<string>(type: "TEXT", nullable: true),
                    ip_origem = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TentativaLogin", x => x.id_tentativa);
                    table.ForeignKey(
                        name: "FK_TentativaLogin_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "Alerta",
                columns: table => new
                {
                    id_alerta = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_paciente = table.Column<long>(type: "INTEGER", nullable: false),
                    mensagem = table.Column<string>(type: "TEXT", nullable: false),
                    data_hora = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerta", x => x.id_alerta);
                    table.ForeignKey(
                        name: "FK_Alerta_Paciente_id_paciente",
                        column: x => x.id_paciente,
                        principalTable: "Paciente",
                        principalColumn: "id_paciente");
                });

            migrationBuilder.CreateTable(
                name: "HistoricoMedico",
                columns: table => new
                {
                    id_historico = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_paciente = table.Column<long>(type: "INTEGER", nullable: false),
                    diagnosticos_passados = table.Column<string>(type: "TEXT", nullable: true),
                    alergias = table.Column<string>(type: "TEXT", nullable: true),
                    intervencoes = table.Column<string>(type: "TEXT", nullable: true),
                    procedimentos = table.Column<string>(type: "TEXT", nullable: true),
                    atualizado_em = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoMedico", x => x.id_historico);
                    table.ForeignKey(
                        name: "FK_HistoricoMedico_Paciente_id_paciente",
                        column: x => x.id_paciente,
                        principalTable: "Paciente",
                        principalColumn: "id_paciente");
                });

            migrationBuilder.CreateTable(
                name: "Medicacao",
                columns: table => new
                {
                    id_medicacao = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_paciente = table.Column<long>(type: "INTEGER", nullable: false),
                    nome = table.Column<string>(type: "TEXT", nullable: false),
                    dosagem = table.Column<string>(type: "TEXT", nullable: false),
                    frequencia = table.Column<string>(type: "TEXT", nullable: true),
                    horario_administracao = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicacao", x => x.id_medicacao);
                    table.ForeignKey(
                        name: "FK_Medicacao_Paciente_id_paciente",
                        column: x => x.id_paciente,
                        principalTable: "Paciente",
                        principalColumn: "id_paciente");
                });

            migrationBuilder.CreateTable(
                name: "MonitoramentoSaude",
                columns: table => new
                {
                    id_monitoramento = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_paciente = table.Column<long>(type: "INTEGER", nullable: false),
                    frequencia_cardiaca = table.Column<int>(type: "INTEGER", nullable: true),
                    pressao_arterial = table.Column<string>(type: "TEXT", nullable: true),
                    temperatura = table.Column<decimal>(type: "DECIMAL(4,1)", nullable: true),
                    glicose = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: true),
                    data_hora = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    id_dispositivo = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoramentoSaude", x => x.id_monitoramento);
                    table.ForeignKey(
                        name: "FK_MonitoramentoSaude_Dispositivo_id_dispositivo",
                        column: x => x.id_dispositivo,
                        principalTable: "Dispositivo",
                        principalColumn: "id_dispositivo");
                    table.ForeignKey(
                        name: "FK_MonitoramentoSaude_Paciente_id_paciente",
                        column: x => x.id_paciente,
                        principalTable: "Paciente",
                        principalColumn: "id_paciente");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alerta_id_paciente",
                table: "Alerta",
                column: "id_paciente");

            migrationBuilder.CreateIndex(
                name: "IX_Cuidador_id_usuario",
                table: "Cuidador",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoMedico_id_paciente",
                table: "HistoricoMedico",
                column: "id_paciente",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LogAuditoria_id_usuario",
                table: "LogAuditoria",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Medicacao_id_paciente",
                table: "Medicacao",
                column: "id_paciente");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoramentoSaude_id_dispositivo",
                table: "MonitoramentoSaude",
                column: "id_dispositivo");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoramentoSaude_id_paciente",
                table: "MonitoramentoSaude",
                column: "id_paciente");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_id_usuario",
                table: "Paciente",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_SessaoUsuario_id_usuario",
                table: "SessaoUsuario",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_TentativaLogin_id_usuario",
                table: "TentativaLogin",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerta");

            migrationBuilder.DropTable(
                name: "Cuidador");

            migrationBuilder.DropTable(
                name: "HistoricoMedico");

            migrationBuilder.DropTable(
                name: "LogAuditoria");

            migrationBuilder.DropTable(
                name: "Medicacao");

            migrationBuilder.DropTable(
                name: "MonitoramentoSaude");

            migrationBuilder.DropTable(
                name: "SessaoUsuario");

            migrationBuilder.DropTable(
                name: "TentativaLogin");

            migrationBuilder.DropTable(
                name: "Dispositivo");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
