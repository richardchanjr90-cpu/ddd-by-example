using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class improved_location : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Location",
                schema: "loyalty");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "loyalty",
                table: "Venue",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "loyalty",
                table: "Venue",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Latitude",
                schema: "loyalty",
                table: "Venue",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Longitude",
                schema: "loyalty",
                table: "Venue",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "loyalty",
                table: "Venue");

            migrationBuilder.DropColumn(
                name: "City",
                schema: "loyalty",
                table: "Venue");

            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "loyalty",
                table: "Venue");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "loyalty",
                table: "Venue");

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(maxLength: 200, nullable: false),
                    City = table.Column<string>(maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    IsArchived = table.Column<bool>(nullable: false),
                    Latitude = table.Column<float>(nullable: false),
                    Longitude = table.Column<float>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    VenueId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Venue_VenueId",
                        column: x => x.VenueId,
                        principalSchema: "loyalty",
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Location_VenueId",
                schema: "loyalty",
                table: "Location",
                column: "VenueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_Longitude_Latitude",
                schema: "loyalty",
                table: "Location",
                columns: new[] { "Longitude", "Latitude" },
                unique: true);
        }
    }
}
