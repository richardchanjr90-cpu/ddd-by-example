using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                "loyalty");

            migrationBuilder.CreateTable(
                "Venue",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    FranchiseId = table.Column<long>(nullable: true),
                    IsPublished = table.Column<bool>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    LogoUrl = table.Column<string>(nullable: true),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "Location",
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
                        "FK_Location_Venue_VenueId",
                        x => x.VenueId,
                        principalSchema: "loyalty",
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "LoyaltyProgram",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false),
                    VenueId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyaltyProgram", x => x.Id);
                    table.ForeignKey(
                        "FK_LoyaltyProgram_Venue_VenueId",
                        x => x.VenueId,
                        principalSchema: "loyalty",
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "VenueCategory",
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
                    CategoryType = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                "VenueDetails",
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
                    FullDescription = table.Column<string>(nullable: true),
                    Phones = table.Column<string>(nullable: true),
                    WebSites = table.Column<string>(nullable: true),
                    WorkingHours = table.Column<string>(nullable: true),
                    PhotosUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueDetails", x => x.Id);
                    table.ForeignKey(
                        "FK_VenueDetails_Venue_VenueId",
                        x => x.VenueId,
                        principalSchema: "loyalty",
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "LoyaltyProductGroup",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LoyaltyProgramId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsArchived = table.Column<bool>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyaltyProductGroup", x => x.Id);
                    table.ForeignKey(
                        "FK_LoyaltyProductGroup_LoyaltyProgram_LoyaltyProgramId",
                        x => x.LoyaltyProgramId,
                        principalSchema: "loyalty",
                        principalTable: "LoyaltyProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Card",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LoyaltyProductId = table.Column<long>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false),
                    LoyaltyProductGroupId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                    table.ForeignKey(
                        "FK_Card_LoyaltyProductGroup_LoyaltyProductGroupId",
                        x => x.LoyaltyProductGroupId,
                        principalSchema: "loyalty",
                        principalTable: "LoyaltyProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "LoyaltyProduct",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LoyaltyProductGroupId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Rule = table.Column<string>(nullable: true),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyaltyProduct", x => x.Id);
                    table.ForeignKey(
                        "FK_LoyaltyProduct_LoyaltyProductGroup_LoyaltyProductGroupId",
                        x => x.LoyaltyProductGroupId,
                        principalSchema: "loyalty",
                        principalTable: "LoyaltyProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Purchase",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CardId = table.Column<long>(nullable: false),
                    BurnDate = table.Column<DateTime>(nullable: true),
                    Value = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.Id);
                    table.ForeignKey(
                        "FK_Purchase_Card_CardId",
                        x => x.CardId,
                        principalSchema: "loyalty",
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Card_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "Card",
                column: "LoyaltyProductGroupId");

            migrationBuilder.CreateIndex(
                "IX_Location_VenueId",
                schema: "loyalty",
                table: "Location",
                column: "VenueId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_LoyaltyProduct_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                column: "LoyaltyProductGroupId");

            migrationBuilder.CreateIndex(
                "IX_LoyaltyProductGroup_LoyaltyProgramId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                column: "LoyaltyProgramId");

            migrationBuilder.CreateIndex(
                "IX_LoyaltyProgram_VenueId",
                schema: "loyalty",
                table: "LoyaltyProgram",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                "IX_Purchase_CardId",
                schema: "loyalty",
                table: "Purchase",
                column: "CardId");

            migrationBuilder.CreateIndex(
                "IX_VenueCategory_VenueId",
                schema: "loyalty",
                table: "VenueCategory",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                "IX_VenueDetails_VenueId",
                schema: "loyalty",
                table: "VenueDetails",
                column: "VenueId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Location",
                "loyalty");

            migrationBuilder.DropTable(
                "LoyaltyProduct",
                "loyalty");

            migrationBuilder.DropTable(
                "Purchase",
                "loyalty");

            migrationBuilder.DropTable(
                "VenueCategory",
                "loyalty");

            migrationBuilder.DropTable(
                "VenueDetails",
                "loyalty");

            migrationBuilder.DropTable(
                "Card",
                "loyalty");

            migrationBuilder.DropTable(
                "LoyaltyProductGroup",
                "loyalty");

            migrationBuilder.DropTable(
                "LoyaltyProgram",
                "loyalty");

            migrationBuilder.DropTable(
                "Venue",
                "loyalty");
        }
    }
}
