using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "LoyaltyGroupRule",
                newName: "LoyaltyGroupRule",
                newSchema: "loyalty");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "LoyaltyGroupRule",
                schema: "loyalty",
                newName: "LoyaltyGroupRule");
        }
    }
}
