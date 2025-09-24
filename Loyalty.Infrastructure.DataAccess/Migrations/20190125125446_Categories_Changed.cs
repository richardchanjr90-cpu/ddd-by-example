using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class Categories_Changed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "VenueCategory",
                "loyalty");

            migrationBuilder.AddColumn<int>(
                "CategoryType",
                schema: "loyalty",
                table: "Venue",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "CategoryType",
                schema: "loyalty",
                table: "Venue");

            migrationBuilder.CreateTable(
                "VenueCategory",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryType = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    VenueId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueCategory", x => x.Id);
                    table.ForeignKey(
                        "FK_VenueCategory_Venue_VenueId",
                        x => x.VenueId,
                        principalSchema: "loyalty",
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_VenueCategory_VenueId",
                schema: "loyalty",
                table: "VenueCategory",
                column: "VenueId");
        }
    }
}
