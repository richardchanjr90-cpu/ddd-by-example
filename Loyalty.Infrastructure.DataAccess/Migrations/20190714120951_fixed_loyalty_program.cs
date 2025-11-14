using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class fixed_loyalty_program : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "loyalty",
                table: "LoyaltyProgram",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProgram_Id_Name",
                schema: "loyalty",
                table: "LoyaltyProgram",
                columns: new[] { "Id", "Name" },
                unique: true,
                filter: "[IsArchived] = 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoyaltyProgram_Id_Name",
                schema: "loyalty",
                table: "LoyaltyProgram");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "loyalty",
                table: "LoyaltyProgram",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
