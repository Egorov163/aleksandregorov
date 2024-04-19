using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebStocks.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dividend_Stock_StockId",
                table: "Dividend");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stock",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dividend",
                table: "Dividend");

            migrationBuilder.RenameTable(
                name: "Stock",
                newName: "Stocks");

            migrationBuilder.RenameTable(
                name: "Dividend",
                newName: "Dividends");

            migrationBuilder.RenameIndex(
                name: "IX_Dividend_StockId",
                table: "Dividends",
                newName: "IX_Dividends_StockId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dividends",
                table: "Dividends",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dividends_Stocks_StockId",
                table: "Dividends",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dividends_Stocks_StockId",
                table: "Dividends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dividends",
                table: "Dividends");

            migrationBuilder.RenameTable(
                name: "Stocks",
                newName: "Stock");

            migrationBuilder.RenameTable(
                name: "Dividends",
                newName: "Dividend");

            migrationBuilder.RenameIndex(
                name: "IX_Dividends_StockId",
                table: "Dividend",
                newName: "IX_Dividend_StockId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stock",
                table: "Stock",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dividend",
                table: "Dividend",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dividend_Stock_StockId",
                table: "Dividend",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "Id");
        }
    }
}
