using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class AddedStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_VenueWorker_Id",
                schema: "loyalty",
                table: "VenueWorker");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                schema: "loyalty",
                table: "Venue");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                schema: "loyalty",
                table: "Venue");

            migrationBuilder.AlterColumn<long>(
                name: "CategoryType",
                schema: "loyalty",
                table: "Venue",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "VenueStatus",
                schema: "loyalty",
                table: "Venue",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VenueStatus",
                schema: "loyalty",
                table: "Venue");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryType",
                schema: "loyalty",
                table: "Venue",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                schema: "loyalty",
                table: "Venue",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                schema: "loyalty",
                table: "Venue",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_VenueWorker_Id",
                schema: "loyalty",
                table: "VenueWorker",
                column: "Id");
        }
    }
}
