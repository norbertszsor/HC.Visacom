using Microsoft.EntityFrameworkCore.Migrations;

namespace HotChocolateAPI.Migrations
{
    public partial class changeKeeOrderAndCostam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductsForOrders_OrderId",
                table: "ProductsForOrders");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsForOrders_OrderId",
                table: "ProductsForOrders",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductsForOrders_OrderId",
                table: "ProductsForOrders");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsForOrders_OrderId",
                table: "ProductsForOrders",
                column: "OrderId",
                unique: true);
        }
    }
}
