using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class FixedProduct2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Venue_VenueId",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Venue_VenueId",
                schema: "loyalty",
                table: "Product",
                column: "VenueId",
                principalSchema: "loyalty",
                principalTable: "Venue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Venue_VenueId",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Venue_VenueId",
                schema: "loyalty",
                table: "Product",
                column: "VenueId",
                principalSchema: "loyalty",
                principalTable: "Venue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
