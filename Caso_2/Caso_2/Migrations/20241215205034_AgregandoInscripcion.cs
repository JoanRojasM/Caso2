using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caso_2.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoInscripcion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Asistio",
                table: "Inscripciones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e866178f-b456-4d18-ad9b-4f71b14cf298", "AQAAAAIAAYagAAAAEF9xybeJqhH9ogZvToG6T5mcvM9qoZLgdDlr53N3JuB6QARKIuNVbez4Ak5jk2bP5w==", "fb914d9a-fb9c-44a7-9b3f-c12c0ea625f6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5dd5b45e-251e-4070-8806-0fa9f69ba083", "AQAAAAIAAYagAAAAELDs3P0zNx/nzs8bIZIdLUMb2NBDRPmbilT9k7gVHVaJssO5ZLomwRGd0Tpt/ulODg==", "c420a1c3-3caf-4211-8017-f3fcb02099e1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "744aee1d-ab78-426b-8623-d41bbb85e332", "AQAAAAIAAYagAAAAEFGxhAEqIzrF/h0ZEN5ZsRNzOj7DaYQmfCgivOoDtfJyqeLA2WiHMtfSVaZAxeWKPw==", "a07d448a-d8df-4084-88f2-e17f44123c08" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Asistio",
                table: "Inscripciones");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7a9d4ba1-61c2-4120-b4d3-6cae26d83b62", "AQAAAAIAAYagAAAAEEF8kpptON1iTVqjEy9dZEDXoibkZE6XqUSEjq/majhknmIEoUtC/SUcaaXsDHBhCA==", "b2b1b0cb-506f-474b-b341-4f980fd85563" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "75956723-c62e-4959-a44e-4ef67dd946a2", "AQAAAAIAAYagAAAAEOlC+taMTyLbKcB28IljDj4lXZ1DzcMDYp3YoVC0tfL2QgKoqkrzwlg8oFZMo6jS/Q==", "7e949058-ea10-4aca-8025-2a00ef164b62" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "984cd5aa-fbbb-4dfb-bda5-3e5950a98b07", "AQAAAAIAAYagAAAAENxqllBRI+4TlcqR/8SVdQDxqIzqsm2J3ru+vzSzveb3pYbPEkteERB3PE2CN6Ipfg==", "def971b9-1dc7-4c02-8150-2598c3f5e6a5" });
        }
    }
}
