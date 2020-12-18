using Microsoft.EntityFrameworkCore.Migrations;

namespace AdCampaign.DAL.Migrations
{
    public partial class AddImpressAlways : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ImpressingAlways",
                table: "Adverts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImpressingAlways",
                table: "Adverts");
        }
    }
}
