using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdCampaign.DAL.Migrations
{
    public partial class AddImpressionTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ImpressingDateFrom",
                table: "Adverts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ImpressingDateTo",
                table: "Adverts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ImpressingTimeFrom",
                table: "Adverts",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ImpressingTimeTo",
                table: "Adverts",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImpressingDateFrom",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "ImpressingDateTo",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "ImpressingTimeFrom",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "ImpressingTimeTo",
                table: "Adverts");
        }
    }
}
