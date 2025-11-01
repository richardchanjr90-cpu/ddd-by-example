using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class removed_worker_guid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worker_WorkerId_VenueId",
                schema: "loyalty",
                table: "Worker");

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkerId",
                schema: "loyalty",
                table: "Worker",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Worker_WorkerId_VenueId",
                schema: "loyalty",
                table: "Worker",
                columns: new[] { "WorkerId", "VenueId" },
                unique: true,
                filter: "[WorkerId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worker_WorkerId_VenueId",
                schema: "loyalty",
                table: "Worker");

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkerId",
                schema: "loyalty",
                table: "Worker",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Worker_WorkerId_VenueId",
                schema: "loyalty",
                table: "Worker",
                columns: new[] { "WorkerId", "VenueId" },
                unique: true);
        }
    }
}
