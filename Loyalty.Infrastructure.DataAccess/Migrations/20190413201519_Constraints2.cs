using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class Constraints2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worker_WorkerId",
                schema: "loyalty",
                table: "Worker");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_WorkerId_VenueId",
                schema: "loyalty",
                table: "Worker",
                columns: new[] { "WorkerId", "VenueId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worker_WorkerId_VenueId",
                schema: "loyalty",
                table: "Worker");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_WorkerId",
                schema: "loyalty",
                table: "Worker",
                column: "WorkerId",
                unique: true);
        }
    }
}
