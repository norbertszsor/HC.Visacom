using Microsoft.EntityFrameworkCore.Migrations;

namespace HotChocolateAPI.Migrations
{
    public partial class AddedAmountPropertyToProductOfOrdersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "ProductsForOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "ProductsForOrders");
        }
    }
}
