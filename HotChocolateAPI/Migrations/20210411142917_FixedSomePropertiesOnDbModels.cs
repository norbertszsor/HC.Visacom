using Microsoft.EntityFrameworkCore.Migrations;

namespace HotChocolateAPI.Migrations
{
    public partial class FixedSomePropertiesOnDbModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opinions_Products_ProductId",
                table: "Opinions");

            migrationBuilder.DropIndex(
                name: "IX_Opinions_ProductId",
                table: "Opinions");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Opinions");

            migrationBuilder.AddColumn<int>(
                name: "OpinionId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_OpinionId",
                table: "Products",
                column: "OpinionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Opinions_OpinionId",
                table: "Products",
                column: "OpinionId",
                principalTable: "Opinions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Opinions_OpinionId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OpinionId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OpinionId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Opinions",
                type: "int",
                nullable: true);

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
                onDelete: ReferentialAction.Restrict);
        }
    }
}
