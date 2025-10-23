using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class UpdatedGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoyaltyGroupRule_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "LoyaltyGroupRule");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyGroupRule_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "LoyaltyGroupRule",
                column: "LoyaltyProductGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoyaltyGroupRule_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "LoyaltyGroupRule");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyGroupRule_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "LoyaltyGroupRule",
                column: "LoyaltyProductGroupId",
                unique: true);
        }
    }
}
