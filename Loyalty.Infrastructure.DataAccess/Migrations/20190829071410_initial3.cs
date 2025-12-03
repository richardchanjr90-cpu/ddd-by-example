using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worker_Phone",
                schema: "loyalty",
                table: "Worker");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "loyalty",
                table: "Worker",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VenueId",
                schema: "loyalty",
                table: "Purchase",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Worker_Email",
                schema: "loyalty",
                table: "Worker",
                column: "Email",
                unique: true,
                filter: "([Email] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_Phone",
                schema: "loyalty",
                table: "Worker",
                column: "Phone",
                unique: true,
                filter: "[Phone] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worker_Email",
                schema: "loyalty",
                table: "Worker");

            migrationBuilder.DropIndex(
                name: "IX_Worker_Phone",
                schema: "loyalty",
                table: "Worker");

            migrationBuilder.DropColumn(
                name: "VenueId",
                schema: "loyalty",
                table: "Purchase");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "loyalty",
                table: "Worker",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Worker_Phone",
                schema: "loyalty",
                table: "Worker",
                column: "Phone");
        }
    }
}
