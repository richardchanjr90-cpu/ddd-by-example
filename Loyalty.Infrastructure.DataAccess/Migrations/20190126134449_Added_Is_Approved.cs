using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class Added_Is_Approved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "FranchiseId",
                schema: "loyalty",
                table: "Venue",
                newName: "ParentId");

            migrationBuilder.AlterColumn<string>(
                "WorkingHours",
                schema: "loyalty",
                table: "VenueDetails",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "PhotosUrl",
                schema: "loyalty",
                table: "VenueDetails",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Phones",
                schema: "loyalty",
                table: "VenueDetails",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "FullDescription",
                schema: "loyalty",
                table: "VenueDetails",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Name",
                schema: "loyalty",
                table: "Venue",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Description",
                schema: "loyalty",
                table: "Venue",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                "IsApproved",
                schema: "loyalty",
                table: "Venue",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                "Name",
                schema: "loyalty",
                table: "LoyaltyProgram",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Description",
                schema: "loyalty",
                table: "LoyaltyProgram",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Name",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Description",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Name",
                schema: "loyalty",
                table: "LoyaltyProduct",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Description",
                schema: "loyalty",
                table: "LoyaltyProduct",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "BusinessRule",
                schema: "loyalty",
                table: "LoyaltyProduct",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "City",
                schema: "loyalty",
                table: "Location",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "IsApproved",
                schema: "loyalty",
                table: "Venue");

            migrationBuilder.RenameColumn(
                "ParentId",
                schema: "loyalty",
                table: "Venue",
                newName: "FranchiseId");

            migrationBuilder.AlterColumn<string>(
                "WorkingHours",
                schema: "loyalty",
                table: "VenueDetails",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                "PhotosUrl",
                schema: "loyalty",
                table: "VenueDetails",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                "Phones",
                schema: "loyalty",
                table: "VenueDetails",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                "FullDescription",
                schema: "loyalty",
                table: "VenueDetails",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4000);

            migrationBuilder.AlterColumn<string>(
                "Name",
                schema: "loyalty",
                table: "Venue",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                "Description",
                schema: "loyalty",
                table: "Venue",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Name",
                schema: "loyalty",
                table: "LoyaltyProgram",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                "Description",
                schema: "loyalty",
                table: "LoyaltyProgram",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Name",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                "Description",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                "Name",
                schema: "loyalty",
                table: "LoyaltyProduct",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                "Description",
                schema: "loyalty",
                table: "LoyaltyProduct",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                "BusinessRule",
                schema: "loyalty",
                table: "LoyaltyProduct",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                "City",
                schema: "loyalty",
                table: "Location",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);
        }
    }
}
