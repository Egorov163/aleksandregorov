using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebStocks.Migrations
{
    /// <inheritdoc />
    public partial class AddDividendOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Dividends",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dividends_OwnerId",
                table: "Dividends",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dividends_Users_OwnerId",
                table: "Dividends",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dividends_Users_OwnerId",
                table: "Dividends");

            migrationBuilder.DropIndex(
                name: "IX_Dividends_OwnerId",
                table: "Dividends");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Dividends");
        }
    }
}
