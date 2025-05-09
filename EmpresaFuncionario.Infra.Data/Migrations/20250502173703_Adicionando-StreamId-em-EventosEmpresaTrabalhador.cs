using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmpresasFuncionarios.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoStreamIdemEventosEmpresaTrabalhador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EventosEmpresaFuncionario",
                newName: "StreamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreamId",
                table: "EventosEmpresaFuncionario",
                newName: "Id");
        }
    }
}
