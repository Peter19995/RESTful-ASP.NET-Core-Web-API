using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVill_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class VilaDataUt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 3, 22, 12, 5, 37, 102, DateTimeKind.Local).AddTicks(2842), new DateTime(2023, 3, 22, 12, 5, 37, 102, DateTimeKind.Local).AddTicks(2854) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
