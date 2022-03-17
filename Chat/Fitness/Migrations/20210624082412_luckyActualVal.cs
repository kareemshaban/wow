using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Migrations
{
    public partial class luckyActualVal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ActualValue",
                table: "Lucky",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualValue",
                table: "Lucky");
        }
    }
}
