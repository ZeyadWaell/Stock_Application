using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastraction.Migrations
{
    public partial class FixingIssues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StocksHolder_AppUser_AppUserId",
                table: "StocksHolder");

            migrationBuilder.DropIndex(
                name: "IX_StocksHolder_AppUserId",
                table: "StocksHolder");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "StocksHolder");

            migrationBuilder.DropColumn(
                name: "StockSymbol",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "StocksHolder",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_StocksHolder_UserId",
                table: "StocksHolder",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StocksHolder_AppUser_UserId",
                table: "StocksHolder",
                column: "UserId",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StocksHolder_AppUser_UserId",
                table: "StocksHolder");

            migrationBuilder.DropIndex(
                name: "IX_StocksHolder_UserId",
                table: "StocksHolder");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StocksHolder");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "StocksHolder",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StockSymbol",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_StocksHolder_AppUserId",
                table: "StocksHolder",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StocksHolder_AppUser_AppUserId",
                table: "StocksHolder",
                column: "AppUserId",
                principalTable: "AppUser",
                principalColumn: "Id");
        }
    }
}
