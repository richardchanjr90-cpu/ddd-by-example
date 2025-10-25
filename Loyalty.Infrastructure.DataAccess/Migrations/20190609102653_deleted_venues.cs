using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class deleted_venues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VenueDetails",
                schema: "loyalty");

            migrationBuilder.AddColumn<string>(
                name: "FullDescription",
                schema: "loyalty",
                table: "Venue",
                maxLength: 4000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phones",
                schema: "loyalty",
                table: "Venue",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WebSites",
                schema: "loyalty",
                table: "Venue",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkingHours",
                schema: "loyalty",
                table: "Venue",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullDescription",
                schema: "loyalty",
                table: "Venue");

            migrationBuilder.DropColumn(
                name: "Phones",
                schema: "loyalty",
                table: "Venue");

            migrationBuilder.DropColumn(
                name: "WebSites",
                schema: "loyalty",
                table: "Venue");

            migrationBuilder.DropColumn(
                name: "WorkingHours",
                schema: "loyalty",
                table: "Venue");

            migrationBuilder.CreateTable(
                name: "VenueDetails",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    FullDescription = table.Column<string>(maxLength: 4000, nullable: false),
                    IsArchived = table.Column<bool>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Phones = table.Column<string>(nullable: false),
                    VenueId = table.Column<long>(nullable: false),
                    WebSites = table.Column<string>(nullable: true),
                    WorkingHours = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VenueDetails_Venue_VenueId",
                        column: x => x.VenueId,
                        principalSchema: "loyalty",
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VenueDetails_VenueId",
                schema: "loyalty",
                table: "VenueDetails",
                column: "VenueId",
                unique: true);
        }
    }
}
