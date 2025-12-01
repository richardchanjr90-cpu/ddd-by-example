using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "loyalty");

            migrationBuilder.CreateTable(
                name: "Venue",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    OwnerId = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    ParentId = table.Column<long>(nullable: true),
                    City = table.Column<string>(maxLength: 200, nullable: true),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    Latitude = table.Column<float>(nullable: true),
                    Longitude = table.Column<float>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    CategoryType = table.Column<int>(nullable: false),
                    LogoUrl = table.Column<string>(maxLength: 200, nullable: true),
                    FullDescription = table.Column<string>(maxLength: 4000, nullable: true),
                    Phones = table.Column<string>(nullable: true),
                    WebSites = table.Column<string>(nullable: true),
                    WorkingHours = table.Column<string>(nullable: true),
                    Images = table.Column<string>(nullable: true),
                    IsArchived = table.Column<bool>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoyaltyProgram",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    VenueId = table.Column<long>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyaltyProgram", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoyaltyProgram_Venue_VenueId",
                        column: x => x.VenueId,
                        principalSchema: "loyalty",
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductGroup",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    VenueId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Icon = table.Column<int>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductGroup_Venue_VenueId",
                        column: x => x.VenueId,
                        principalSchema: "loyalty",
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Worker",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    VenueId = table.Column<long>(nullable: false),
                    WorkerId = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhotoUri = table.Column<string>(nullable: true),
                    PositionName = table.Column<string>(nullable: true),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Worker_Venue_VenueId",
                        column: x => x.VenueId,
                        principalSchema: "loyalty",
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoyaltyProductGroup",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LoyaltyProgramId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProductGroupId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyaltyProductGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoyaltyProductGroup_LoyaltyProgram_LoyaltyProgramId",
                        column: x => x.LoyaltyProgramId,
                        principalSchema: "loyalty",
                        principalTable: "LoyaltyProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoyaltyProductGroup_ProductGroup_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalSchema: "loyalty",
                        principalTable: "ProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Icon = table.Column<int>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false),
                    ProductGroupId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductGroup_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalSchema: "loyalty",
                        principalTable: "ProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoyaltyGroupRule",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LoyaltyProductGroupId = table.Column<long>(nullable: false),
                    RuleType = table.Column<int>(nullable: false),
                    Rule = table.Column<string>(nullable: true),
                    RuleVersion = table.Column<int>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyaltyGroupRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoyaltyGroupRule_LoyaltyProductGroup_LoyaltyProductGroupId",
                        column: x => x.LoyaltyProductGroupId,
                        principalSchema: "loyalty",
                        principalTable: "LoyaltyProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Purchase",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LoyaltyProductGroupId = table.Column<long>(nullable: false),
                    ProductId = table.Column<long>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    InternalPurchaseMadeBySystem = table.Column<bool>(nullable: false),
                    Value = table.Column<decimal>(nullable: true),
                    BurnDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchase_LoyaltyProductGroup_LoyaltyProductGroupId",
                        column: x => x.LoyaltyProductGroupId,
                        principalSchema: "loyalty",
                        principalTable: "LoyaltyProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyGroupRule_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "LoyaltyGroupRule",
                column: "LoyaltyProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProductGroup_ProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProductGroup_LoyaltyProgramId_Name",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                columns: new[] { "LoyaltyProgramId", "Name" },
                unique: true,
                filter: "[IsArchived] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProgram_VenueId_Name",
                schema: "loyalty",
                table: "LoyaltyProgram",
                columns: new[] { "VenueId", "Name" },
                unique: true,
                filter: "[IsArchived] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductGroupId_Name",
                schema: "loyalty",
                table: "Product",
                columns: new[] { "ProductGroupId", "Name" },
                unique: true,
                filter: "[IsArchived] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_VenueId_Name",
                schema: "loyalty",
                table: "ProductGroup",
                columns: new[] { "VenueId", "Name" },
                unique: true,
                filter: "[IsArchived] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "Purchase",
                column: "LoyaltyProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_Phone",
                schema: "loyalty",
                table: "Worker",
                column: "Phone",
                unique: true,
                filter: "[IsArchived] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_VenueId",
                schema: "loyalty",
                table: "Worker",
                column: "VenueId");

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
            migrationBuilder.DropTable(
                name: "LoyaltyGroupRule",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "Purchase",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "Worker",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "LoyaltyProductGroup",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "LoyaltyProgram",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "ProductGroup",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "Venue",
                schema: "loyalty");
        }
    }
}
