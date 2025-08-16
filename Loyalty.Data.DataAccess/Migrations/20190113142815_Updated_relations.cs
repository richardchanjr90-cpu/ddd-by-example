using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Data.DataAccess.Migrations
{
    public partial class Updated_relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeoPosition",
                schema: "loyalty");

            migrationBuilder.DropIndex(
                name: "IX_Card_LoyaltyProductId",
                schema: "loyalty",
                table: "Card");

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                schema: "loyalty",
                table: "VenueDetails",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                schema: "loyalty",
                table: "VenueDetails",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                schema: "loyalty",
                table: "Venue",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                schema: "loyalty",
                table: "Venue",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                schema: "loyalty",
                table: "Venue",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                schema: "loyalty",
                table: "Purchase",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                schema: "loyalty",
                table: "Purchase",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                schema: "loyalty",
                table: "LoyaltyProgram",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                schema: "loyalty",
                table: "LoyaltyProgram",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                schema: "loyalty",
                table: "LoyaltyProduct",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                schema: "loyalty",
                table: "LoyaltyProduct",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                schema: "loyalty",
                table: "Card",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                schema: "loyalty",
                table: "Card",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    VenueId = table.Column<long>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    Latitude = table.Column<float>(nullable: false),
                    Longitude = table.Column<float>(nullable: false)
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
                name: "IX_Card_LoyaltyProductId",
                schema: "loyalty",
                table: "Card",
                column: "LoyaltyProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_VenueId",
                schema: "loyalty",
                table: "Location",
                column: "VenueId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Location",
                schema: "loyalty");

            migrationBuilder.DropIndex(
                name: "IX_Card_LoyaltyProductId",
                schema: "loyalty",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                schema: "loyalty",
                table: "Venue");

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                schema: "loyalty",
                table: "VenueDetails",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                schema: "loyalty",
                table: "VenueDetails",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                schema: "loyalty",
                table: "Venue",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                schema: "loyalty",
                table: "Venue",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                schema: "loyalty",
                table: "Purchase",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                schema: "loyalty",
                table: "Purchase",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                schema: "loyalty",
                table: "LoyaltyProgram",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                schema: "loyalty",
                table: "LoyaltyProgram",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                schema: "loyalty",
                table: "LoyaltyProduct",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                schema: "loyalty",
                table: "LoyaltyProduct",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                schema: "loyalty",
                table: "Card",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                schema: "loyalty",
                table: "Card",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "GeoPosition",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    Latitude = table.Column<float>(nullable: false),
                    Longitude = table.Column<float>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    VenueId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoPosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeoPosition_Venue_VenueId",
                        column: x => x.VenueId,
                        principalSchema: "loyalty",
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_LoyaltyProductId",
                schema: "loyalty",
                table: "Card",
                column: "LoyaltyProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeoPosition_VenueId",
                schema: "loyalty",
                table: "GeoPosition",
                column: "VenueId",
                unique: true);
        }
    }
}
