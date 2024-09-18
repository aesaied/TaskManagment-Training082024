using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagment.Migrations
{
    /// <inheritdoc />
    public partial class AddFullNameRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "685837a2-bf15-48a2-96ee-5ded267f6875");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CountryId", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5843edf9-a9de-4f50-a1c3-b468ffebfd9b", 0, "4a9a146c-eb79-4350-b222-ef9625cb730a", 1, "admin@experts.ps", false, "system admin", false, null, "ADMIN@EXPERTS.PS", "ADMIN@EXPERTS.PS", "AQAAAAIAAYagAAAAECH3srUkiNzKjTlXyJwIrcEbxeYERQpQhJDLlKzp8m84vqPWb1+2z6BLFWOJS5ivAw==", null, false, "ac0a71a0-2292-47d2-884a-846159e9210e", false, "admin@experts.ps" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5843edf9-a9de-4f50-a1c3-b468ffebfd9b");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CountryId", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "685837a2-bf15-48a2-96ee-5ded267f6875", 0, "0b40095f-9054-49c2-b7c2-219408b06cb2", 1, "admin@experts.ps", false, "system admin", false, null, "ADMIN@EXPERTS.PS", "ADMIN@EXPERTS.PS", "AQAAAAIAAYagAAAAEGjQgk2j41xor8hbTmxn6RLl+6/BzFbFurFnNlEGGm37KVhR0rV5UufDSkXxkoV82A==", null, false, "cbe2fdd0-6038-4145-b51c-e2a86020d658", false, "admin@experts.ps" });
        }
    }
}
