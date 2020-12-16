using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AdCampaign.DAL.Migrations
{
    public partial class AddFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PrimaryImageId",
                table: "Adverts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SecondaryImageId",
                table: "Adverts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adverts_PrimaryImageId",
                table: "Adverts",
                column: "PrimaryImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Adverts_SecondaryImageId",
                table: "Adverts",
                column: "SecondaryImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_Files_PrimaryImageId",
                table: "Adverts",
                column: "PrimaryImageId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_Files_SecondaryImageId",
                table: "Adverts",
                column: "SecondaryImageId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_Files_PrimaryImageId",
                table: "Adverts");

            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_Files_SecondaryImageId",
                table: "Adverts");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Adverts_PrimaryImageId",
                table: "Adverts");

            migrationBuilder.DropIndex(
                name: "IX_Adverts_SecondaryImageId",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "PrimaryImageId",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "SecondaryImageId",
                table: "Adverts");
        }
    }
}
