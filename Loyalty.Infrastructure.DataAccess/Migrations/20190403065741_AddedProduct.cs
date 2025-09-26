using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class AddedProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_LoyaltyProduct_LoyaltyProductGroup_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProduct");

            migrationBuilder.DropForeignKey(
                "FK_LoyaltyProgramRule_LoyaltyProgram_LoyaltyProgram",
                schema: "loyalty",
                table: "LoyaltyProgramRule");

            migrationBuilder.DropForeignKey(
                "FK_Purchase_Card_CardId",
                schema: "loyalty",
                table: "Purchase");

            migrationBuilder.DropTable(
                "Card",
                "loyalty");

            migrationBuilder.DropTable(
                "LoyaltyProductGroup",
                "loyalty");

            migrationBuilder.DropIndex(
                "IX_Purchase_CardId",
                schema: "loyalty",
                table: "Purchase");

            migrationBuilder.DropIndex(
                "IX_LoyaltyProgramRule_LoyaltyProgram",
                schema: "loyalty",
                table: "LoyaltyProgramRule");

            migrationBuilder.DropColumn(
                "PhotosUrl",
                schema: "loyalty",
                table: "VenueDetails");

            migrationBuilder.DropColumn(
                "CardId",
                schema: "loyalty",
                table: "Purchase");

            migrationBuilder.DropColumn(
                "LoyaltyProgram",
                schema: "loyalty",
                table: "LoyaltyProgramRule");

            migrationBuilder.DropColumn(
                "Name",
                schema: "loyalty",
                table: "LoyaltyProduct");

            migrationBuilder.RenameColumn(
                "LoyaltyProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                newName: "LoyaltyProgramId");

            migrationBuilder.RenameIndex(
                "IX_LoyaltyProduct_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                newName: "IX_LoyaltyProduct_LoyaltyProgramId");

            migrationBuilder.AddColumn<Guid>(
                "UserId",
                schema: "loyalty",
                table: "Purchase",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<long>(
                "ProductId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                "RuleId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                nullable: true);

            migrationBuilder.CreateTable(
                "Product",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Icon = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                "IX_LoyaltyProduct_ProductId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                "IX_LoyaltyProduct_RuleId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                column: "RuleId");

            migrationBuilder.AddForeignKey(
                "FK_LoyaltyProduct_LoyaltyProgram_LoyaltyProgramId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                column: "LoyaltyProgramId",
                principalSchema: "loyalty",
                principalTable: "LoyaltyProgram",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_LoyaltyProduct_Product_ProductId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                column: "ProductId",
                principalSchema: "loyalty",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_LoyaltyProduct_LoyaltyProgramRule_RuleId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                column: "RuleId",
                principalSchema: "loyalty",
                principalTable: "LoyaltyProgramRule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_LoyaltyProduct_LoyaltyProgram_LoyaltyProgramId",
                schema: "loyalty",
                table: "LoyaltyProduct");

            migrationBuilder.DropForeignKey(
                "FK_LoyaltyProduct_Product_ProductId",
                schema: "loyalty",
                table: "LoyaltyProduct");

            migrationBuilder.DropForeignKey(
                "FK_LoyaltyProduct_LoyaltyProgramRule_RuleId",
                schema: "loyalty",
                table: "LoyaltyProduct");

            migrationBuilder.DropTable(
                "Product",
                "loyalty");

            migrationBuilder.DropIndex(
                "IX_LoyaltyProduct_ProductId",
                schema: "loyalty",
                table: "LoyaltyProduct");

            migrationBuilder.DropIndex(
                "IX_LoyaltyProduct_RuleId",
                schema: "loyalty",
                table: "LoyaltyProduct");

            migrationBuilder.DropColumn(
                "UserId",
                schema: "loyalty",
                table: "Purchase");

            migrationBuilder.DropColumn(
                "ProductId",
                schema: "loyalty",
                table: "LoyaltyProduct");

            migrationBuilder.DropColumn(
                "RuleId",
                schema: "loyalty",
                table: "LoyaltyProduct");

            migrationBuilder.RenameColumn(
                "LoyaltyProgramId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                newName: "LoyaltyProductGroupId");

            migrationBuilder.RenameIndex(
                "IX_LoyaltyProduct_LoyaltyProgramId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                newName: "IX_LoyaltyProduct_LoyaltyProductGroupId");

            migrationBuilder.AddColumn<string>(
                "PhotosUrl",
                schema: "loyalty",
                table: "VenueDetails",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                "CardId",
                schema: "loyalty",
                table: "Purchase",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                "LoyaltyProgram",
                schema: "loyalty",
                table: "LoyaltyProgramRule",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                "Name",
                schema: "loyalty",
                table: "LoyaltyProduct",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                "LoyaltyProductGroup",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    IsArchived = table.Column<bool>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false),
                    LoyaltyProgramId = table.Column<long>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
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
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    IsArchived = table.Column<bool>(nullable: false),
                    LoyaltyProductGroupId = table.Column<long>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Purchase_CardId",
                schema: "loyalty",
                table: "Purchase",
                column: "CardId");

            migrationBuilder.CreateIndex(
                "IX_LoyaltyProgramRule_LoyaltyProgram",
                schema: "loyalty",
                table: "LoyaltyProgramRule",
                column: "LoyaltyProgram",
                unique: true,
                filter: "[LoyaltyProgram] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_Card_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "Card",
                column: "LoyaltyProductGroupId");

            migrationBuilder.CreateIndex(
                "IX_LoyaltyProductGroup_LoyaltyProgramId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                column: "LoyaltyProgramId");

            migrationBuilder.AddForeignKey(
                "FK_LoyaltyProduct_LoyaltyProductGroup_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                column: "LoyaltyProductGroupId",
                principalSchema: "loyalty",
                principalTable: "LoyaltyProductGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_LoyaltyProgramRule_LoyaltyProgram_LoyaltyProgram",
                schema: "loyalty",
                table: "LoyaltyProgramRule",
                column: "LoyaltyProgram",
                principalSchema: "loyalty",
                principalTable: "LoyaltyProgram",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Purchase_Card_CardId",
                schema: "loyalty",
                table: "Purchase",
                column: "CardId",
                principalSchema: "loyalty",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
