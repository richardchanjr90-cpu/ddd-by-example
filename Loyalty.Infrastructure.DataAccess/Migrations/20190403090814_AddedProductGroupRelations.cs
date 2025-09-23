using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class AddedProductGroupRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "VenueId",
                schema: "loyalty",
                table: "Purchase",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProductGroupId",
                schema: "loyalty",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductGroupId",
                schema: "loyalty",
                table: "Product",
                column: "ProductGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductGroup_ProductGroupId",
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
                name: "FK_Product_ProductGroup_ProductGroupId",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductGroupId",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "VenueId",
                schema: "loyalty",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "ProductGroupId",
                schema: "loyalty",
                table: "Product");
        }
    }
}
