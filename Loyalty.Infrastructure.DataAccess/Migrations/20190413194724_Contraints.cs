using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class Contraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoyaltyProductGroup_LoyaltyProgramId",
                schema: "loyalty",
                table: "LoyaltyProductGroup");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "loyalty",
                table: "Worker",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PositionName",
                schema: "loyalty",
                table: "Worker",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Worker_Email",
                schema: "loyalty",
                table: "Worker",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_WorkerId",
                schema: "loyalty",
                table: "Worker",
                column: "WorkerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_VenueId_Name",
                schema: "loyalty",
                table: "ProductGroup",
                columns: new[] { "VenueId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_VenueId_Name",
                schema: "loyalty",
                table: "Product",
                columns: new[] { "VenueId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProductGroup_LoyaltyProgramId_ProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                columns: new[] { "LoyaltyProgramId", "ProductGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_Longitude_Latitude",
                schema: "loyalty",
                table: "Location",
                columns: new[] { "Longitude", "Latitude" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worker_Email",
                schema: "loyalty",
                table: "Worker");

            migrationBuilder.DropIndex(
                name: "IX_Worker_WorkerId",
                schema: "loyalty",
                table: "Worker");

            migrationBuilder.DropIndex(
                name: "IX_ProductGroup_VenueId_Name",
                schema: "loyalty",
                table: "ProductGroup");

            migrationBuilder.DropIndex(
                name: "IX_Product_VenueId_Name",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_LoyaltyProductGroup_LoyaltyProgramId_ProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup");

            migrationBuilder.DropIndex(
                name: "IX_Location_Longitude_Latitude",
                schema: "loyalty",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "PositionName",
                schema: "loyalty",
                table: "Worker");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "loyalty",
                table: "Worker",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProductGroup_LoyaltyProgramId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                column: "LoyaltyProgramId");
        }
    }
}
