using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVill_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserNmae : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "LocalUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "LocalUser");
        }
    }
}
