using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class AddedProductGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoyaltyProduct",
                schema: "loyalty");

            migrationBuilder.AddColumn<long>(
                name: "VenueId",
                schema: "loyalty",
                table: "Product",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "VenueId",
                schema: "loyalty",
                table: "LoyaltyProgramRule",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ProductGroup",
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
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Icon = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoyaltyProductGroup",
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
                    RuleId = table.Column<long>(nullable: true),
                    ProductGroupId = table.Column<long>(nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoyaltyProductGroup_LoyaltyProgramRule_RuleId",
                        column: x => x.RuleId,
                        principalSchema: "loyalty",
                        principalTable: "LoyaltyProgramRule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProductGroup_LoyaltyProgramId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                column: "LoyaltyProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProductGroup_ProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProductGroup_RuleId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                column: "RuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoyaltyProductGroup",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "ProductGroup",
                schema: "loyalty");

            migrationBuilder.DropColumn(
                name: "VenueId",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "VenueId",
                schema: "loyalty",
                table: "LoyaltyProgramRule");

            migrationBuilder.CreateTable(
                name: "LoyaltyProduct",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    IsArchived = table.Column<bool>(nullable: false),
                    LoyaltyProgramId = table.Column<long>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    ProductId = table.Column<long>(nullable: true),
                    RuleId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyaltyProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoyaltyProduct_LoyaltyProgram_LoyaltyProgramId",
                        column: x => x.LoyaltyProgramId,
                        principalSchema: "loyalty",
                        principalTable: "LoyaltyProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoyaltyProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "loyalty",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoyaltyProduct_LoyaltyProgramRule_RuleId",
                        column: x => x.RuleId,
                        principalSchema: "loyalty",
                        principalTable: "LoyaltyProgramRule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProduct_LoyaltyProgramId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                column: "LoyaltyProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProduct_ProductId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProduct_RuleId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                column: "RuleId");
        }
    }
}
