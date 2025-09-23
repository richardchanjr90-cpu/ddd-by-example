using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class AddedProductGroupRelations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VenueId",
                schema: "loyalty",
                table: "Purchase",
                newName: "LoyaltyProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_LoyaltyProgramId",
                schema: "loyalty",
                table: "Purchase",
                column: "LoyaltyProgramId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_LoyaltyProgram_LoyaltyProgramId",
                schema: "loyalty",
                table: "Purchase");

            migrationBuilder.DropIndex(
                name: "IX_Purchase_LoyaltyProgramId",
                schema: "loyalty",
                table: "Purchase");

            migrationBuilder.RenameColumn(
                name: "LoyaltyProgramId",
                schema: "loyalty",
                table: "Purchase",
                newName: "VenueId");
        }
    }
}
