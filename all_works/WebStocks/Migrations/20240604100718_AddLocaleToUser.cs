using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebStocks.Migrations
{
    /// <inheritdoc />
    public partial class AddLocaleToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreferLocale",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "en");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferLocale",
                table: "Users");
        }
    }
}
