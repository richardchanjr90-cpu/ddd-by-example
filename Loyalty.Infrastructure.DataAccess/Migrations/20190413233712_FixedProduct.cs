using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class FixedProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Product_Venue_VenueId",
                schema: "loyalty",
                table: "Product",
                column: "VenueId",
                principalSchema: "loyalty",
                principalTable: "Venue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroup_Venue_VenueId",
                schema: "loyalty",
                table: "ProductGroup",
                column: "VenueId",
                principalSchema: "loyalty",
                principalTable: "Venue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Venue_VenueId",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroup_Venue_VenueId",
                schema: "loyalty",
                table: "ProductGroup");
        }
    }
}
