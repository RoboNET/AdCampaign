using Microsoft.EntityFrameworkCore.Migrations;

namespace AdCampaign.DAL.Migrations
{
    public partial class FixConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_BlockedById",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Adverts_BlockedById",
                table: "Adverts");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BlockedById",
                table: "Users",
                column: "BlockedById");

            migrationBuilder.CreateIndex(
                name: "IX_Adverts_BlockedById",
                table: "Adverts",
                column: "BlockedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_BlockedById",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Adverts_BlockedById",
                table: "Adverts");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BlockedById",
                table: "Users",
                column: "BlockedById",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Adverts_BlockedById",
                table: "Adverts",
                column: "BlockedById",
                unique: true);
        }
    }
}
