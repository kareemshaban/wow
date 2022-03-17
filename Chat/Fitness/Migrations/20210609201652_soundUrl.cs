using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Migrations
{
    public partial class soundUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SoundUrl",
                table: "Gift",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoundUrl",
                table: "Gift");
        }
    }
}
