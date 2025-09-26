using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class AddedProductGroupRelations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "VenueId",
                schema: "loyalty",
                table: "Purchase",
                newName: "LoyaltyProgramId");

            migrationBuilder.CreateIndex(
                "IX_Purchase_LoyaltyProgramId",
                schema: "loyalty",
                table: "Purchase",
                column: "LoyaltyProgramId");

            migrationBuilder.AddForeignKey(
                "FK_Purchase_LoyaltyProgram_LoyaltyProgramId",
                schema: "loyalty",
                table: "Purchase",
                column: "LoyaltyProgramId",
                principalSchema: "loyalty",
                principalTable: "LoyaltyProgram",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Purchase_LoyaltyProgram_LoyaltyProgramId",
                schema: "loyalty",
                table: "Purchase");

            migrationBuilder.DropIndex(
                "IX_Purchase_LoyaltyProgramId",
                schema: "loyalty",
                table: "Purchase");

            migrationBuilder.RenameColumn(
                "LoyaltyProgramId",
                schema: "loyalty",
                table: "Purchase",
                newName: "VenueId");
        }
    }
}
