using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VenueWorker_Worker_VenueId",
                schema: "loyalty",
                table: "VenueWorker");

            migrationBuilder.CreateIndex(
                name: "IX_VenueWorker_WorkerId",
                schema: "loyalty",
                table: "VenueWorker",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_VenueWorker_Worker_WorkerId",
                schema: "loyalty",
                table: "VenueWorker",
                column: "WorkerId",
                principalSchema: "loyalty",
                principalTable: "Worker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VenueWorker_Worker_WorkerId",
                schema: "loyalty",
                table: "VenueWorker");

            migrationBuilder.DropIndex(
                name: "IX_VenueWorker_WorkerId",
                schema: "loyalty",
                table: "VenueWorker");

            migrationBuilder.AddForeignKey(
                name: "FK_VenueWorker_Worker_VenueId",
                schema: "loyalty",
                table: "VenueWorker",
                column: "VenueId",
                principalSchema: "loyalty",
                principalTable: "Worker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
