using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class AddedArchivedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                "IsArchived",
                schema: "loyalty",
                table: "VenueDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                "IsArchived",
                schema: "loyalty",
                table: "Location",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "IsArchived",
                schema: "loyalty",
                table: "VenueDetails");

            migrationBuilder.DropColumn(
                "IsArchived",
                schema: "loyalty",
                table: "Location");
        }
    }
}
