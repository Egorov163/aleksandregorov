using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebStocks.Migrations
{
    /// <inheritdoc />
    public partial class deleteRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Users_OwnerId",
                table: "Stocks");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Stocks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Users_OwnerId",
                table: "Stocks",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Users_OwnerId",
                table: "Stocks");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Users_OwnerId",
                table: "Stocks",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
