using Microsoft.EntityFrameworkCore.Migrations;

namespace HotChocolateAPI.Migrations
{
    public partial class changeKeeOrderAndCostam2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opinions_Products_ProductId",
                table: "Opinions");

            migrationBuilder.DropIndex(
                name: "IX_Opinions_ProductId",
                table: "Opinions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Opinions_ProductId",
                table: "Opinions",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Opinions_Products_ProductId",
                table: "Opinions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
