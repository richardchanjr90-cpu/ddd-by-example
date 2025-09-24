using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class AddedProductGroupRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                "VenueId",
                schema: "loyalty",
                table: "Purchase",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                "ProductGroupId",
                schema: "loyalty",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_Product_ProductGroupId",
                schema: "loyalty",
                table: "Product",
                column: "ProductGroupId");

            migrationBuilder.AddForeignKey(
                "FK_Product_ProductGroup_ProductGroupId",
                schema: "loyalty",
                table: "Product",
                column: "ProductGroupId",
                principalSchema: "loyalty",
                principalTable: "ProductGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Product_ProductGroup_ProductGroupId",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropIndex(
                "IX_Product_ProductGroupId",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropColumn(
                "VenueId",
                schema: "loyalty",
                table: "Purchase");

            migrationBuilder.DropColumn(
                "ProductGroupId",
                schema: "loyalty",
                table: "Product");
        }
    }
}
