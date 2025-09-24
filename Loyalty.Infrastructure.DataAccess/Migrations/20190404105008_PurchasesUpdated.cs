using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class PurchasesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_LoyaltyProgram_LoyaltyProgramId",
                schema: "loyalty",
                table: "Purchase");

            migrationBuilder.RenameColumn(
                name: "LoyaltyProgramId",
                schema: "loyalty",
                table: "Purchase",
                newName: "LoyaltyProductGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchase_LoyaltyProgramId",
                schema: "loyalty",
                table: "Purchase",
                newName: "IX_Purchase_LoyaltyProductGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_LoyaltyProductGroup_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "Purchase",
                column: "LoyaltyProductGroupId",
                principalSchema: "loyalty",
                principalTable: "LoyaltyProductGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_LoyaltyProductGroup_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "Purchase");

            migrationBuilder.RenameColumn(
                name: "LoyaltyProductGroupId",
                schema: "loyalty",
                table: "Purchase",
                newName: "LoyaltyProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchase_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "Purchase",
                newName: "IX_Purchase_LoyaltyProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_LoyaltyProgram_LoyaltyProgramId",
                schema: "loyalty",
                table: "Purchase",
                column: "LoyaltyProgramId",
                principalSchema: "loyalty",
                principalTable: "LoyaltyProgram",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
