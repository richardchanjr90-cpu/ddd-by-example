using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Data.DataAccess.Migrations
{
    public partial class Db_Relations_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Card_CardId",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases");

            migrationBuilder.RenameTable(
                name: "Purchases",
                newName: "Purchase",
                newSchema: "loyalty");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_CardId",
                schema: "loyalty",
                table: "Purchase",
                newName: "IX_Purchase_CardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchase",
                schema: "loyalty",
                table: "Purchase",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VenueDetails_VenueId",
                schema: "loyalty",
                table: "VenueDetails",
                column: "VenueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProgram_VenueId",
                schema: "loyalty",
                table: "LoyaltyProgram",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_LoyaltyProductId",
                schema: "loyalty",
                table: "Card",
                column: "LoyaltyProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_LoyaltyProduct_LoyaltyProductId",
                schema: "loyalty",
                table: "Card",
                column: "LoyaltyProductId",
                principalSchema: "loyalty",
                principalTable: "LoyaltyProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoyaltyProgram_Venue_VenueId",
                schema: "loyalty",
                table: "LoyaltyProgram",
                column: "VenueId",
                principalSchema: "loyalty",
                principalTable: "Venue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_Card_CardId",
                schema: "loyalty",
                table: "Purchase",
                column: "CardId",
                principalSchema: "loyalty",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VenueDetails_Venue_VenueId",
                schema: "loyalty",
                table: "VenueDetails",
                column: "VenueId",
                principalSchema: "loyalty",
                principalTable: "Venue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_LoyaltyProduct_LoyaltyProductId",
                schema: "loyalty",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_LoyaltyProgram_Venue_VenueId",
                schema: "loyalty",
                table: "LoyaltyProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_Card_CardId",
                schema: "loyalty",
                table: "Purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_VenueDetails_Venue_VenueId",
                schema: "loyalty",
                table: "VenueDetails");

            migrationBuilder.DropIndex(
                name: "IX_VenueDetails_VenueId",
                schema: "loyalty",
                table: "VenueDetails");

            migrationBuilder.DropIndex(
                name: "IX_LoyaltyProgram_VenueId",
                schema: "loyalty",
                table: "LoyaltyProgram");

            migrationBuilder.DropIndex(
                name: "IX_Card_LoyaltyProductId",
                schema: "loyalty",
                table: "Card");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchase",
                schema: "loyalty",
                table: "Purchase");

            migrationBuilder.RenameTable(
                name: "Purchase",
                schema: "loyalty",
                newName: "Purchases");

            migrationBuilder.RenameIndex(
                name: "IX_Purchase_CardId",
                table: "Purchases",
                newName: "IX_Purchases_CardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Card_CardId",
                table: "Purchases",
                column: "CardId",
                principalSchema: "loyalty",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
