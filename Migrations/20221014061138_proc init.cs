using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStoreSara.Migrations
{
    public partial class procinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE SelectMonthSales AS SELECT YEAR(bH.billAddDateAndTime) AS Year, 
                    MONTH(bH.billAddDateAndTime) AS Month, SUM(bD.billPrice) AS Total_Sales FROM billDetail bD 
                    inner join billheader bH on bD.billHeaderID = bH.billHeaderId 
                    GROUP BY YEAR(bH.billAddDateAndTime), MONTH(bH.billAddDateAndTime)";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
