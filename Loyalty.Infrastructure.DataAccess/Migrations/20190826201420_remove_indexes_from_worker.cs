using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class remove_indexes_from_worker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worker_Phone",
                schema: "loyalty",
                table: "Worker");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_Phone",
                schema: "loyalty",
                table: "Worker",
                column: "Phone");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_WorkerId",
                schema: "loyalty",
                table: "Worker",
                column: "WorkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worker_Phone",
                schema: "loyalty",
                table: "Worker");

            migrationBuilder.DropIndex(
                name: "IX_Worker_WorkerId",
                schema: "loyalty",
                table: "Worker");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_Phone",
                schema: "loyalty",
                table: "Worker",
                column: "Phone",
                unique: true,
                filter: "[IsArchived] = 0");
        }
    }
}
