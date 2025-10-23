using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class UpdatedProductGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Venue_VenueId",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductGroupId",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_VenueId_Name",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "VenueId",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.AlterColumn<long>(
                name: "ProductGroupId",
                schema: "loyalty",
                table: "Product",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OwnerVenueId",
                schema: "loyalty",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_OwnerVenueId",
                schema: "loyalty",
                table: "Product",
                column: "OwnerVenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductGroupId_Name",
                schema: "loyalty",
                table: "Product",
                columns: new[] { "ProductGroupId", "Name" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Venue_OwnerVenueId",
                schema: "loyalty",
                table: "Product",
                column: "OwnerVenueId",
                principalSchema: "loyalty",
                principalTable: "Venue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Venue_OwnerVenueId",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_OwnerVenueId",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductGroupId_Name",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "OwnerVenueId",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.AlterColumn<long>(
                name: "ProductGroupId",
                schema: "loyalty",
                table: "Product",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "VenueId",
                schema: "loyalty",
                table: "Product",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductGroupId",
                schema: "loyalty",
                table: "Product",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_VenueId_Name",
                schema: "loyalty",
                table: "Product",
                columns: new[] { "VenueId", "Name" },
                unique: true);

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
    }
}
