using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStoreSara.Migrations
{
    public partial class Billheaderdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_billDetail_billHeader_billHeaderID",
                table: "billDetail");

            migrationBuilder.DropIndex(
                name: "IX_billDetail_billHeaderID",
                table: "billDetail");

            migrationBuilder.AddColumn<DateTime>(
                name: "billAddDateAndTime",
                table: "billHeader",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "billAddDateAndTime",
                table: "billHeader");

            migrationBuilder.CreateIndex(
                name: "IX_billDetail_billHeaderID",
                table: "billDetail",
                column: "billHeaderID");

            migrationBuilder.AddForeignKey(
                name: "FK_billDetail_billHeader_billHeaderID",
                table: "billDetail",
                column: "billHeaderID",
                principalTable: "billHeader",
                principalColumn: "billHeaderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
