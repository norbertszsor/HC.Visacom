using Microsoft.EntityFrameworkCore.Migrations;

namespace HotChocolateAPI.Migrations
{
    public partial class ChangeOpinion3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Opinions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Opinions");
        }
    }
}
