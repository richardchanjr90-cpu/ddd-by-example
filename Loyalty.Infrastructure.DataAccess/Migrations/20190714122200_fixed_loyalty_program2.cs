using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class fixed_loyalty_program2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoyaltyProgram_VenueId",
                schema: "loyalty",
                table: "LoyaltyProgram");

            migrationBuilder.DropIndex(
                name: "IX_LoyaltyProgram_Id_Name",
                schema: "loyalty",
                table: "LoyaltyProgram");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProgram_VenueId_Name",
                schema: "loyalty",
                table: "LoyaltyProgram",
                columns: new[] { "VenueId", "Name" },
                unique: true,
                filter: "[IsArchived] = 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoyaltyProgram_VenueId_Name",
                schema: "loyalty",
                table: "LoyaltyProgram");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProgram_VenueId",
                schema: "loyalty",
                table: "LoyaltyProgram",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProgram_Id_Name",
                schema: "loyalty",
                table: "LoyaltyProgram",
                columns: new[] { "Id", "Name" },
                unique: true,
                filter: "[IsArchived] = 0");
        }
    }
}
