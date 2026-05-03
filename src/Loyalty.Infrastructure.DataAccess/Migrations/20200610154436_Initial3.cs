using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoyaltyGroupRule_LoyaltyProductGroup_LoyaltyProductGroupId1",
                schema: "loyalty",
                table: "LoyaltyGroupRule");

            migrationBuilder.DropIndex(
                name: "IX_LoyaltyGroupRule_LoyaltyProductGroupId1",
                schema: "loyalty",
                table: "LoyaltyGroupRule");

            migrationBuilder.DropColumn(
                name: "LoyaltyProductGroupId1",
                schema: "loyalty",
                table: "LoyaltyGroupRule");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LoyaltyProductGroupId1",
                schema: "loyalty",
                table: "LoyaltyGroupRule",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyGroupRule_LoyaltyProductGroupId1",
                schema: "loyalty",
                table: "LoyaltyGroupRule",
                column: "LoyaltyProductGroupId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LoyaltyGroupRule_LoyaltyProductGroup_LoyaltyProductGroupId1",
                schema: "loyalty",
                table: "LoyaltyGroupRule",
                column: "LoyaltyProductGroupId1",
                principalSchema: "loyalty",
                principalTable: "LoyaltyProductGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
