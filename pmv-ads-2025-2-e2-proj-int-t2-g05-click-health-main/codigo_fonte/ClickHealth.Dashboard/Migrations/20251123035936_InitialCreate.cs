using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClickHealth.Dashboard.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    status_conexao = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispositivo", x => x.id_dispositivo);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    SenhaHash = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    FotoPath = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Cuidador",
                columns: table => new
                {
                    id_cuidador = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_usuario = table.Column<int>(type: "INTEGER", nullable: false),
                    nome = table.Column<string>(type: "TEXT", nullable: false),
                    tipo = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuidador", x => x.id_cuidador);
                    table.ForeignKey(
                        name: "FK_Cuidador_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogAuditoria",
                columns: table => new
                {
                    id_log = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_usuario = table.Column<int>(type: "INTEGER", nullable: false),
                    acao = table.Column<string>(type: "TEXT", nullable: false),
                    data_hora = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    descricao = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogAuditoria", x => x.id_log);
                    table.ForeignKey(
                        name: "FK_LogAuditoria_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    id_paciente = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_usuario = table.Column<int>(type: "INTEGER", nullable: false),
                    condicoes_medicas = table.Column<string>(type: "TEXT", nullable: true),
                    dados_pessoais = table.Column<string>(type: "TEXT", nullable: true),
                    data_nascimento = table.Column<DateTime>(type: "TEXT", nullable: true),
                    telefone = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.id_paciente);
                    table.ForeignKey(
                        name: "FK_Paciente_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "SessaoUsuario",
                columns: table => new
                {
                    id_sessao = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_usuario = table.Column<int>(type: "INTEGER", nullable: false),
                    token_sessao = table.Column<string>(type: "TEXT", nullable: false),
                    data_inicio = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    data_fim = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessaoUsuario", x => x.id_sessao);
                    table.ForeignKey(
                        name: "FK_SessaoUsuario_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TentativaLogin",
                columns: table => new
                {
                    id_tentativa = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_usuario = table.Column<int>(type: "INTEGER", nullable: false),
                    data_hora = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    resultado = table.Column<string>(type: "TEXT", nullable: true),
                    ip_origem = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TentativaLogin", x => x.id_tentativa);
                    table.ForeignKey(
                        name: "FK_TentativaLogin_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alerta",
                columns: table => new
                {
                    id_alerta = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_paciente = table.Column<int>(type: "INTEGER", nullable: false),
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
                    id_paciente = table.Column<int>(type: "INTEGER", nullable: false),
                    diagnosticos_passados = table.Column<string>(type: "TEXT", nullable: true),
                    alergias = table.Column<string>(type: "TEXT", nullable: true),
                    intervencoes = table.Column<string>(type: "TEXT", nullable: true),
                    procedimentos = table.Column<string>(type: "TEXT", nullable: true),
                    atualizado_em = table.Column<DateTime>(type: "DATETIME", nullable: true)
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
                    nome = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    dosagem = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    instrucoes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    principio_ativo = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    data_cadastro = table.Column<DateTime>(type: "TEXT", nullable: true),
                    id_paciente = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicacao", x => x.id_medicacao);
                    table.ForeignKey(
                        name: "FK_Medicacao_Paciente_id_paciente",
                        column: x => x.id_paciente,
                        principalTable: "Paciente",
                        principalColumn: "id_paciente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonitoramentoSaude",
                columns: table => new
                {
                    id_monitoramento = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_paciente = table.Column<int>(type: "INTEGER", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Notificacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    Mensagem = table.Column<string>(type: "TEXT", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    DataHora = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Lida = table.Column<bool>(type: "INTEGER", nullable: false),
                    id_paciente = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificacoes_Paciente_id_paciente",
                        column: x => x.id_paciente,
                        principalTable: "Paciente",
                        principalColumn: "id_paciente",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "RegistroClinico",
                columns: table => new
                {
                    id_registro = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_paciente = table.Column<int>(type: "INTEGER", nullable: false),
                    data_registro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    resumo = table.Column<string>(type: "TEXT", nullable: true),
                    observacoes = table.Column<string>(type: "TEXT", nullable: true),
                    exames_json = table.Column<string>(type: "TEXT", nullable: true),
                    arquivo_path = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroClinico", x => x.id_registro);
                    table.ForeignKey(
                        name: "FK_RegistroClinico_Paciente_id_paciente",
                        column: x => x.id_paciente,
                        principalTable: "Paciente",
                        principalColumn: "id_paciente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgendamentoMedicacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdPaciente = table.Column<int>(type: "INTEGER", nullable: false),
                    IdMedicacao = table.Column<int>(type: "INTEGER", nullable: false),
                    DataHora = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Dose = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Observacoes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendamentoMedicacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgendamentoMedicacao_Medicacao_IdMedicacao",
                        column: x => x.IdMedicacao,
                        principalTable: "Medicacao",
                        principalColumn: "id_medicacao",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgendamentoMedicacao_Paciente_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Paciente",
                        principalColumn: "id_paciente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgendamentoMedicacao_IdMedicacao",
                table: "AgendamentoMedicacao",
                column: "IdMedicacao");

            migrationBuilder.CreateIndex(
                name: "IX_AgendamentoMedicacao_IdPaciente",
                table: "AgendamentoMedicacao",
                column: "IdPaciente");

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
                name: "IX_Notificacoes_id_paciente",
                table: "Notificacoes",
                column: "id_paciente");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_id_usuario",
                table: "Paciente",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroClinico_id_paciente",
                table: "RegistroClinico",
                column: "id_paciente");

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
                name: "AgendamentoMedicacao");

            migrationBuilder.DropTable(
                name: "Alerta");

            migrationBuilder.DropTable(
                name: "Cuidador");

            migrationBuilder.DropTable(
                name: "HistoricoMedico");

            migrationBuilder.DropTable(
                name: "LogAuditoria");

            migrationBuilder.DropTable(
                name: "MonitoramentoSaude");

            migrationBuilder.DropTable(
                name: "Notificacoes");

            migrationBuilder.DropTable(
                name: "RegistroClinico");

            migrationBuilder.DropTable(
                name: "SessaoUsuario");

            migrationBuilder.DropTable(
                name: "TentativaLogin");

            migrationBuilder.DropTable(
                name: "Medicacao");

            migrationBuilder.DropTable(
                name: "Dispositivo");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
