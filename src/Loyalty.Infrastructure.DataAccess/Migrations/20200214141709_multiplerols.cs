using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class multiplerols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionName",
                schema: "loyalty",
                table: "Worker");

            migrationBuilder.AddColumn<string>(
                name: "PositionName",
                schema: "loyalty",
                table: "VenueWorker",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Icon",
                schema: "loyalty",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionName",
                schema: "loyalty",
                table: "VenueWorker");

            migrationBuilder.AddColumn<string>(
                name: "PositionName",
                schema: "loyalty",
                table: "Worker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Icon",
                schema: "loyalty",
                table: "Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true);
        }
    }
}
