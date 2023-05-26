using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVill_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class VillaNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.CreateTable(
                name: "VillaNumbers",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillaNumbers", x => x.VillaNo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VillaNumbers");

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdateDate" },
                values: new object[] { 1, "", new DateTime(2023, 3, 22, 12, 5, 37, 102, DateTimeKind.Local).AddTicks(2842), "Multiple Active Result Sets (MARS) is a feature that works with SQL Server to allow the execution of multiple batches on a single connection. When MARS is enabled for use with SQL Server, each command object used adds a session to the connection", "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg", "Test", 5, 5.0, 5, new DateTime(2023, 3, 22, 12, 5, 37, 102, DateTimeKind.Local).AddTicks(2854) });
        }
    }
}
