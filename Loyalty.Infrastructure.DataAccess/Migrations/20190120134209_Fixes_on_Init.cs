using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class Fixes_on_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_LoyaltyProductGroup_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "LoyaltyProductId",
                schema: "loyalty",
                table: "Card");

            migrationBuilder.RenameColumn(
                name: "Rule",
                schema: "loyalty",
                table: "LoyaltyProduct",
                newName: "BusinessRule");

            migrationBuilder.AlterColumn<long>(
                name: "LoyaltyProductGroupId",
                schema: "loyalty",
                table: "Card",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_LoyaltyProductGroup_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "Card",
                column: "LoyaltyProductGroupId",
                principalSchema: "loyalty",
                principalTable: "LoyaltyProductGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_LoyaltyProductGroup_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "Card");

            migrationBuilder.RenameColumn(
                name: "BusinessRule",
                schema: "loyalty",
                table: "LoyaltyProduct",
                newName: "Rule");

            migrationBuilder.AlterColumn<long>(
                name: "LoyaltyProductGroupId",
                schema: "loyalty",
                table: "Card",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "LoyaltyProductId",
                schema: "loyalty",
                table: "Card",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_LoyaltyProductGroup_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "Card",
                column: "LoyaltyProductGroupId",
                principalSchema: "loyalty",
                principalTable: "LoyaltyProductGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
