using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStoreSara.Migrations
{
    public partial class Initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "billHeader",
                columns: table => new
                {
                    billHeaderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    billFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    billLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    billEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    billAddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    billAddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    billTown = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    billProvince = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_billHeader", x => x.billHeaderId);
                });

            migrationBuilder.CreateTable(
                name: "manufacturers",
                columns: table => new
                {
                    ManufacturerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManufacturerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManufacturerContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManufacturerProductType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManufacturerIsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manufacturers", x => x.ManufacturerID);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userAddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userAddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userTown = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userProvince = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userRePassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "billDetail",
                columns: table => new
                {
                    billDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    billHeaderID = table.Column<int>(type: "int", nullable: false),
                    billProduct = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    billQty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    billPrice = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_billDetail", x => x.billDetailId);
                    table.ForeignKey(
                        name: "FK_billDetail_billHeader_billHeaderID",
                        column: x => x.billHeaderID,
                        principalTable: "billHeader",
                        principalColumn: "billHeaderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPrice = table.Column<double>(type: "float", nullable: false),
                    ProductImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductAddDateAndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ManufacturerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_products_manufacturers_ManufacturerID",
                        column: x => x.ManufacturerID,
                        principalTable: "manufacturers",
                        principalColumn: "ManufacturerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_billDetail_billHeaderID",
                table: "billDetail",
                column: "billHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_products_ManufacturerID",
                table: "products",
                column: "ManufacturerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "billDetail");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "billHeader");

            migrationBuilder.DropTable(
                name: "manufacturers");
        }
    }
}
