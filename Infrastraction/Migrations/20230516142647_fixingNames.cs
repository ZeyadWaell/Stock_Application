using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastraction.Migrations
{
    public partial class fixingNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeleveryTime",
                table: "DeliverMethods",
                newName: "DeliveryTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliveryTime",
                table: "DeliverMethods",
                newName: "DeleveryTime");
        }
    }
}
