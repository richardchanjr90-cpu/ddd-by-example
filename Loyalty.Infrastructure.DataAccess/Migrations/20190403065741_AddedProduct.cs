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
                name: "FK_LoyaltyProduct_LoyaltyProductGroup_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LoyaltyProgramRule_LoyaltyProgram_LoyaltyProgram",
                schema: "loyalty",
                table: "LoyaltyProgramRule");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_Card_CardId",
                schema: "loyalty",
                table: "Purchase");

            migrationBuilder.DropTable(
                name: "Card",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "LoyaltyProductGroup",
                schema: "loyalty");

            migrationBuilder.DropIndex(
                name: "IX_Purchase_CardId",
                schema: "loyalty",
                table: "Purchase");

            migrationBuilder.DropIndex(
                name: "IX_LoyaltyProgramRule_LoyaltyProgram",
                schema: "loyalty",
                table: "LoyaltyProgramRule");

            migrationBuilder.DropColumn(
                name: "PhotosUrl",
                schema: "loyalty",
                table: "VenueDetails");

            migrationBuilder.DropColumn(
                name: "CardId",
                schema: "loyalty",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "LoyaltyProgram",
                schema: "loyalty",
                table: "LoyaltyProgramRule");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "loyalty",
                table: "LoyaltyProduct");

            migrationBuilder.RenameColumn(
                name: "LoyaltyProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                newName: "LoyaltyProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_LoyaltyProduct_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                newName: "IX_LoyaltyProduct_LoyaltyProgramId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "loyalty",
                table: "Purchase",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RuleId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Product",
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
                name: "IX_LoyaltyProduct_ProductId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProduct_RuleId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                column: "RuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoyaltyProduct_LoyaltyProgram_LoyaltyProgramId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                column: "LoyaltyProgramId",
                principalSchema: "loyalty",
                principalTable: "LoyaltyProgram",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoyaltyProduct_Product_ProductId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                column: "ProductId",
                principalSchema: "loyalty",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoyaltyProduct_LoyaltyProgramRule_RuleId",
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
                name: "FK_LoyaltyProduct_LoyaltyProgram_LoyaltyProgramId",
                schema: "loyalty",
                table: "LoyaltyProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LoyaltyProduct_Product_ProductId",
                schema: "loyalty",
                table: "LoyaltyProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LoyaltyProduct_LoyaltyProgramRule_RuleId",
                schema: "loyalty",
                table: "LoyaltyProduct");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "loyalty");

            migrationBuilder.DropIndex(
                name: "IX_LoyaltyProduct_ProductId",
                schema: "loyalty",
                table: "LoyaltyProduct");

            migrationBuilder.DropIndex(
                name: "IX_LoyaltyProduct_RuleId",
                schema: "loyalty",
                table: "LoyaltyProduct");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "loyalty",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "loyalty",
                table: "LoyaltyProduct");

            migrationBuilder.DropColumn(
                name: "RuleId",
                schema: "loyalty",
                table: "LoyaltyProduct");

            migrationBuilder.RenameColumn(
                name: "LoyaltyProgramId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                newName: "LoyaltyProductGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_LoyaltyProduct_LoyaltyProgramId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                newName: "IX_LoyaltyProduct_LoyaltyProductGroupId");

            migrationBuilder.AddColumn<string>(
                name: "PhotosUrl",
                schema: "loyalty",
                table: "VenueDetails",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "CardId",
                schema: "loyalty",
                table: "Purchase",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "LoyaltyProgram",
                schema: "loyalty",
                table: "LoyaltyProgramRule",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "loyalty",
                table: "LoyaltyProduct",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "LoyaltyProductGroup",
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
                        name: "FK_LoyaltyProductGroup_LoyaltyProgram_LoyaltyProgramId",
                        column: x => x.LoyaltyProgramId,
                        principalSchema: "loyalty",
                        principalTable: "LoyaltyProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Card",
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
                        name: "FK_Card_LoyaltyProductGroup_LoyaltyProductGroupId",
                        column: x => x.LoyaltyProductGroupId,
                        principalSchema: "loyalty",
                        principalTable: "LoyaltyProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_CardId",
                schema: "loyalty",
                table: "Purchase",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProgramRule_LoyaltyProgram",
                schema: "loyalty",
                table: "LoyaltyProgramRule",
                column: "LoyaltyProgram",
                unique: true,
                filter: "[LoyaltyProgram] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Card_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "Card",
                column: "LoyaltyProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProductGroup_LoyaltyProgramId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                column: "LoyaltyProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoyaltyProduct_LoyaltyProductGroup_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProduct",
                column: "LoyaltyProductGroupId",
                principalSchema: "loyalty",
                principalTable: "LoyaltyProductGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoyaltyProgramRule_LoyaltyProgram_LoyaltyProgram",
                schema: "loyalty",
                table: "LoyaltyProgramRule",
                column: "LoyaltyProgram",
                principalSchema: "loyalty",
                principalTable: "LoyaltyProgram",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_Card_CardId",
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
