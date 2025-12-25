using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worker_WorkerId",
                schema: "loyalty",
                table: "Worker");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_WorkerId",
                schema: "loyalty",
                table: "Worker",
                column: "WorkerId",
                unique: true,
                filter: "([IsArchived] = 0 AND WorkerId IS NOT NULL)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worker_WorkerId",
                schema: "loyalty",
                table: "Worker");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_WorkerId",
                schema: "loyalty",
                table: "Worker",
                column: "WorkerId",
                unique: true,
                filter: "[IsArchived] = 0");
        }
    }
}
