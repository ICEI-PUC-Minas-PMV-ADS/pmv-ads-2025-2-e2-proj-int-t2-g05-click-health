using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClickHealth.Dashboard.Migrations
{
    /// <inheritdoc />
    public partial class VersaoFinalCorrigida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgendamentoMedicacao_Medicacao_IdMedicacao",
                table: "AgendamentoMedicacao");

            migrationBuilder.DropForeignKey(
                name: "FK_AgendamentoMedicacao_Paciente_IdPaciente",
                table: "AgendamentoMedicacao");

            migrationBuilder.DropTable(
                name: "Medicacao");

            migrationBuilder.DropColumn(
                name: "Observacoes",
                table: "AgendamentoMedicacao");

            migrationBuilder.RenameColumn(
                name: "IdPaciente",
                table: "AgendamentoMedicacao",
                newName: "id_paciente");

            migrationBuilder.RenameColumn(
                name: "IdMedicacao",
                table: "AgendamentoMedicacao",
                newName: "id_medicacao");

            migrationBuilder.RenameIndex(
                name: "IX_AgendamentoMedicacao_IdPaciente",
                table: "AgendamentoMedicacao",
                newName: "IX_AgendamentoMedicacao_id_paciente");

            migrationBuilder.RenameIndex(
                name: "IX_AgendamentoMedicacao_IdMedicacao",
                table: "AgendamentoMedicacao",
                newName: "IX_AgendamentoMedicacao_id_medicacao");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "AgendamentoMedicacao",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instrucoes",
                table: "AgendamentoMedicacao",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Medicacoes",
                columns: table => new
                {
                    id_medicacao = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    dosagem = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    instrucoes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    principio_ativo = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    data_cadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    data_inicio = table.Column<DateTime>(type: "TEXT", nullable: true),
                    data_fim = table.Column<DateTime>(type: "TEXT", nullable: true),
                    id_paciente = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicacoes", x => x.id_medicacao);
                    table.ForeignKey(
                        name: "FK_Medicacoes_Paciente_id_paciente",
                        column: x => x.id_paciente,
                        principalTable: "Paciente",
                        principalColumn: "id_paciente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicacoes_id_paciente",
                table: "Medicacoes",
                column: "id_paciente");

            migrationBuilder.AddForeignKey(
                name: "FK_AgendamentoMedicacao_Medicacoes_id_medicacao",
                table: "AgendamentoMedicacao",
                column: "id_medicacao",
                principalTable: "Medicacoes",
                principalColumn: "id_medicacao",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgendamentoMedicacao_Paciente_id_paciente",
                table: "AgendamentoMedicacao",
                column: "id_paciente",
                principalTable: "Paciente",
                principalColumn: "id_paciente",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgendamentoMedicacao_Medicacoes_id_medicacao",
                table: "AgendamentoMedicacao");

            migrationBuilder.DropForeignKey(
                name: "FK_AgendamentoMedicacao_Paciente_id_paciente",
                table: "AgendamentoMedicacao");

            migrationBuilder.DropTable(
                name: "Medicacoes");

            migrationBuilder.DropColumn(
                name: "Instrucoes",
                table: "AgendamentoMedicacao");

            migrationBuilder.RenameColumn(
                name: "id_paciente",
                table: "AgendamentoMedicacao",
                newName: "IdPaciente");

            migrationBuilder.RenameColumn(
                name: "id_medicacao",
                table: "AgendamentoMedicacao",
                newName: "IdMedicacao");

            migrationBuilder.RenameIndex(
                name: "IX_AgendamentoMedicacao_id_paciente",
                table: "AgendamentoMedicacao",
                newName: "IX_AgendamentoMedicacao_IdPaciente");

            migrationBuilder.RenameIndex(
                name: "IX_AgendamentoMedicacao_id_medicacao",
                table: "AgendamentoMedicacao",
                newName: "IX_AgendamentoMedicacao_IdMedicacao");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "AgendamentoMedicacao",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Observacoes",
                table: "AgendamentoMedicacao",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Medicacao",
                columns: table => new
                {
                    id_medicacao = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_paciente = table.Column<int>(type: "INTEGER", nullable: false),
                    data_cadastro = table.Column<DateTime>(type: "TEXT", nullable: true),
                    dosagem = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    instrucoes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    nome = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    principio_ativo = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Medicacao_id_paciente",
                table: "Medicacao",
                column: "id_paciente");

            migrationBuilder.AddForeignKey(
                name: "FK_AgendamentoMedicacao_Medicacao_IdMedicacao",
                table: "AgendamentoMedicacao",
                column: "IdMedicacao",
                principalTable: "Medicacao",
                principalColumn: "id_medicacao",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgendamentoMedicacao_Paciente_IdPaciente",
                table: "AgendamentoMedicacao",
                column: "IdPaciente",
                principalTable: "Paciente",
                principalColumn: "id_paciente",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
