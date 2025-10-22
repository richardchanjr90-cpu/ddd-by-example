using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class MakePhoneInsteadOfEmailUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worker_Email",
                schema: "loyalty",
                table: "Worker");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                schema: "loyalty",
                table: "Worker",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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
                column: "Phone",
                unique: true,
                filter: "[IsArchived] = 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worker_Phone",
                schema: "loyalty",
                table: "Worker");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                schema: "loyalty",
                table: "Worker",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "loyalty",
                table: "Worker",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Worker_Email",
                schema: "loyalty",
                table: "Worker",
                column: "Email",
                unique: true,
                filter: "[IsArchived] = 0");
        }
    }
}
