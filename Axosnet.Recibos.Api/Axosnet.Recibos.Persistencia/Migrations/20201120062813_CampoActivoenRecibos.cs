using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Axosnet.Recibos.Persistencia.Migrations
{
    public partial class CampoActivoenRecibos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("044c5ce8-14a9-4260-9c4a-b4f7fce5d86f"));

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Recibos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Activo", "Clave", "Email", "Nombre" },
                values: new object[] { new Guid("385f82fa-ae35-4b7b-8154-67be2343c488"), true, "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", "admin@axosnet.com", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("385f82fa-ae35-4b7b-8154-67be2343c488"));

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Recibos");

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Activo", "Clave", "Email", "Nombre" },
                values: new object[] { new Guid("044c5ce8-14a9-4260-9c4a-b4f7fce5d86f"), true, "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", "admin@axosnet.com", "Admin" });
        }
    }
}
