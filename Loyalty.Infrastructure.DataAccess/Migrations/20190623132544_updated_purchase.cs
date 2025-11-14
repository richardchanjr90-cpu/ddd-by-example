using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class updated_purchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                schema: "loyalty",
                table: "Purchase",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RuleVersion",
                schema: "loyalty",
                table: "LoyaltyGroupRule",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "loyalty",
                table: "Purchase");

            migrationBuilder.AlterColumn<string>(
                name: "RuleVersion",
                schema: "loyalty",
                table: "LoyaltyGroupRule",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
