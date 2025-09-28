using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class ChangeGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoyaltyProductGroup_ProductGroup_ProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup");

            migrationBuilder.DropIndex(
                name: "IX_LoyaltyProductGroup_ProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup");

            migrationBuilder.AddColumn<long>(
                name: "LoyaltyProductGroupId",
                schema: "loyalty",
                table: "ProductGroup",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProductGroup_GroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoyaltyProductGroup_ProductGroup_GroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                column: "GroupId",
                principalSchema: "loyalty",
                principalTable: "ProductGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoyaltyProductGroup_ProductGroup_GroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup");

            migrationBuilder.DropIndex(
                name: "IX_LoyaltyProductGroup_GroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup");

            migrationBuilder.DropColumn(
                name: "LoyaltyProductGroupId",
                schema: "loyalty",
                table: "ProductGroup");

            migrationBuilder.DropColumn(
                name: "GroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup");

            migrationBuilder.AlterColumn<long>(
                name: "ProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProductGroup_ProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                column: "ProductGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoyaltyProductGroup_ProductGroup_ProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                column: "ProductGroupId",
                principalSchema: "loyalty",
                principalTable: "ProductGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
