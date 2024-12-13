using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caso_2.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoInscripciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inscripciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FechaInscripcion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscripciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inscripciones_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscripciones_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "be3f4c49-c647-4ef1-999c-2b27b00c9b5d", "AQAAAAIAAYagAAAAEJLYbxD6AnCOH4Ncr3D4WRKMRi+KAsv/Ya8yt9bVk4J0liQ4fz3GkpRNL7A4aVqajA==", "c94631e3-6def-4194-9263-7e93430a04a4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24aebfd6-cbeb-47f2-9a65-948c5cbb6356", "AQAAAAIAAYagAAAAEKR//fFS0OwQtMMdw782jaKWqwGUW6USYNEymzvUxQ0de5XXA+2bYEZdE+opevqwFg==", "66d54575-5d0b-43e6-ad37-6267ee755204" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49af0bd4-6186-46fa-a89b-478a029afb2c", "AQAAAAIAAYagAAAAECmOkTdv4/FjvTJmMNasw/C8/oEEci0Uj40qMpmUnHX6URR5wm4L3K//RI2eijvsRA==", "5264d573-9d14-43b1-8ecd-4c761e168982" });

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_EventoId",
                table: "Inscripciones",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_UsuarioId",
                table: "Inscripciones",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inscripciones");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d0f7fb26-8d5e-4db1-a2e6-f536d1885d61", "AQAAAAIAAYagAAAAEHCNcO39MQ52MWmR8O1grgI2Ip7K8RyA5C3LfHH1PGx+nYQlUTz9410vcWq2QAcsLA==", "e8f10114-a361-40aa-8cda-24593b85be24" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "13434545-6fb8-443f-be76-e86ec85371d4", "AQAAAAIAAYagAAAAEKNVzWQKHLlcmsK086+0lKl4ndVkvN6Ul19ojdXj9RcwSBcotjXF4okWFlvcdrKsCw==", "12e5b3fe-2bdd-40bc-8cb7-77b466cd04a2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4c2b5989-b466-4835-8916-2335f731ad6f", "AQAAAAIAAYagAAAAEOoZTV2FD3QPEHomOuHihuq3bASNI2AuYyMgeiqdK8JWxoueLflZJ/4aT88Vf0lu2g==", "ea656561-52d2-4b26-a591-71b6e8ea7839" });
        }
    }
}
