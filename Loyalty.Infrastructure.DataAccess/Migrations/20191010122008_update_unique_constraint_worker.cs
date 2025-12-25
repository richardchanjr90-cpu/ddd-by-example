using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class update_unique_constraint_worker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VenueWorker_VenueId_WorkerId",
                schema: "loyalty",
                table: "VenueWorker",
                columns: new[] { "VenueId", "WorkerId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VenueWorker_VenueId_WorkerId",
                schema: "loyalty",
                table: "VenueWorker");
        }
    }
}
