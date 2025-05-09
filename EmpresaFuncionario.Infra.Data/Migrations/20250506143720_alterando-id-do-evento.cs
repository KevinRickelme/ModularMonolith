using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmpresasFuncionarios.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class alterandoiddoevento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventosEmpresaFuncionario",
                table: "EventosEmpresaFuncionario");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "EventosEmpresaFuncionario",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventosEmpresaFuncionario",
                table: "EventosEmpresaFuncionario",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventosEmpresaFuncionario",
                table: "EventosEmpresaFuncionario");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EventosEmpresaFuncionario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventosEmpresaFuncionario",
                table: "EventosEmpresaFuncionario",
                column: "StreamId");
        }
    }
}
