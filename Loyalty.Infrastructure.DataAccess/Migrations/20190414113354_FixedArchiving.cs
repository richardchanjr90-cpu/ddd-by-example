using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class FixedArchiving : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Venue_OwnerVenueId",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Worker_Email",
                schema: "loyalty",
                table: "Worker");

            migrationBuilder.DropIndex(
                name: "IX_ProductGroup_VenueId_Name",
                schema: "loyalty",
                table: "ProductGroup");

            migrationBuilder.DropIndex(
                name: "IX_Product_OwnerVenueId",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductGroupId_Name",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_LoyaltyProductGroup_LoyaltyProgramId_ProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup");

            migrationBuilder.DropColumn(
                name: "OwnerVenueId",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_Email",
                schema: "loyalty",
                table: "Worker",
                column: "Email",
                unique: true,
                filter: "[IsArchived] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_VenueId_Name",
                schema: "loyalty",
                table: "ProductGroup",
                columns: new[] { "VenueId", "Name" },
                unique: true,
                filter: "[IsArchived] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductGroupId_Name",
                schema: "loyalty",
                table: "Product",
                columns: new[] { "ProductGroupId", "Name" },
                unique: true,
                filter: "[IsArchived] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProductGroup_LoyaltyProgramId_ProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                columns: new[] { "LoyaltyProgramId", "ProductGroupId" },
                unique: true,
                filter: "[IsArchived] = 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worker_Email",
                schema: "loyalty",
                table: "Worker");

            migrationBuilder.DropIndex(
                name: "IX_ProductGroup_VenueId_Name",
                schema: "loyalty",
                table: "ProductGroup");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductGroupId_Name",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_LoyaltyProductGroup_LoyaltyProgramId_ProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup");

            migrationBuilder.AddColumn<long>(
                name: "OwnerVenueId",
                schema: "loyalty",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Worker_Email",
                schema: "loyalty",
                table: "Worker",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_VenueId_Name",
                schema: "loyalty",
                table: "ProductGroup",
                columns: new[] { "VenueId", "Name" },
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProductGroup_LoyaltyProgramId_ProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                columns: new[] { "LoyaltyProgramId", "ProductGroupId" },
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
    }
}
