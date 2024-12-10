using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caso_2.Migrations
{
    /// <inheritdoc />
    public partial class ModificandoIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NombreCompleto",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "NombreCompleto", "PasswordHash", "Rol", "SecurityStamp", "Telefono" },
                values: new object[] { "1c16f328-c0a5-4a42-b578-6ebdf5e572f8", "Administrador General", "AQAAAAIAAYagAAAAEESxgGPrsmJJ1+jN/iNGN5hIP/GyFIjTehkJOoqTtVZdYhJ7+ak3QNh3IG/jPLz8RA==", "Administrador", "8db2e6bd-189b-4ca9-90b8-ad3951706c91", "123456789" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "NombreCompleto", "PasswordHash", "Rol", "SecurityStamp", "Telefono" },
                values: new object[] { "5c1cc8ee-dc52-41fc-b969-ba68c21785bf", "Organizador Evento", "AQAAAAIAAYagAAAAEB9zogPFZqRLe+6unPEZcD38siYykBOe5JhgZaiaFzoX+i3fBrIBv88bWO9I4/uvSA==", "Organizador", "b26d49ee-7826-426c-9bfd-db15665795e0", "987654321" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "NombreCompleto", "PasswordHash", "Rol", "SecurityStamp", "Telefono" },
                values: new object[] { "180744b2-1afd-475d-90c7-27ecd56ba3a5", "Usuario Regular", "AQAAAAIAAYagAAAAEKDCbd8Z0FiRM4MjumTDFl+N2hXzyW6/6qmQ5FHz47wNQiejkZt7r0cxMKxRu5wo+g==", "Usuario", "d154f0b5-d67c-463c-a735-522d6c781f6a", "1122334455" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreCompleto",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Rol",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c8b767d4-b132-43c2-a09e-db55f96dca87", "AQAAAAIAAYagAAAAEIxiqtqsXex4/rTSb3MDPVe4QOxKpZPoG686Rd/Z+z9qMHLYHNIY64RSeddc6kqY2Q==", "2f5786f0-b5f9-4b3e-9ef1-4df972489c13" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "31fc0b16-4b5c-418a-a8d7-3ffea2a35f4d", "AQAAAAIAAYagAAAAEIdXfLyhpCO9qv2n2MLoOr9FBjtOMJLWe/irgqo7VaFk2fx/ubbUJhdiD+EEgJb3kA==", "34117bb5-e342-4c50-b0b3-51f59fbd4d22" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1de72616-7435-42b6-bc7b-e8b78cd8c3a2", "AQAAAAIAAYagAAAAEA56lRg1RTQ0xCkFxGtjKWK0mBLwrIdSIjpVcS7SPmhmYLM5AyJsGx6sUvHVbSUDeA==", "ac5f0d40-5d68-4d0e-9839-d98384427da2" });
        }
    }
}
