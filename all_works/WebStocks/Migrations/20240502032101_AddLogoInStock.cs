﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebStocks.Migrations
{
    /// <inheritdoc />
    public partial class AddLogoInStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Stocks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Stocks");
        }
    }
}
