using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebStocks.Migrations
{
    /// <inheritdoc />
    public partial class AddStockOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Stocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_OwnerId",
                table: "Stocks",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Users_OwnerId",
                table: "Stocks",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Users_OwnerId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_OwnerId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Stocks");
        }
    }
}
