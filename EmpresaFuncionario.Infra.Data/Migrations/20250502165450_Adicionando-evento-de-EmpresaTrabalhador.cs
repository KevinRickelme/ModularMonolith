using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmpresasFuncionarios.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoeventodeEmpresaTrabalhador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventosEmpresaFuncionario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoEvento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dados = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventosEmpresaFuncionario", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventosEmpresaFuncionario");
        }
    }
}
