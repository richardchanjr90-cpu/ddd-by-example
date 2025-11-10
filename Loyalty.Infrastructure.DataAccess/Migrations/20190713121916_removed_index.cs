using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class removed_index : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoyaltyProductGroup_LoyaltyProgramId_ProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProductGroup_LoyaltyProgramId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                column: "LoyaltyProgramId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoyaltyProductGroup_LoyaltyProgramId",
                schema: "loyalty",
                table: "LoyaltyProductGroup");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProductGroup_LoyaltyProgramId_ProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                columns: new[] { "LoyaltyProgramId", "ProductGroupId" },
                unique: true,
                filter: "[IsArchived] = 0");
        }
    }
}
