using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class ChangedRule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoyaltyProductGroup_LoyaltyProgramRule_RuleId",
                schema: "loyalty",
                table: "LoyaltyProductGroup");

            migrationBuilder.DropTable(
                name: "LoyaltyProgramRule",
                schema: "loyalty");

            migrationBuilder.DropIndex(
                name: "IX_LoyaltyProductGroup_RuleId",
                schema: "loyalty",
                table: "LoyaltyProductGroup");

            migrationBuilder.DropColumn(
                name: "RuleId",
                schema: "loyalty",
                table: "LoyaltyProductGroup");

            migrationBuilder.CreateTable(
                name: "LoyaltyGroupRule",
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
                    RuleType = table.Column<int>(nullable: false),
                    Rule = table.Column<string>(nullable: true),
                    RuleVersion = table.Column<string>(nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyGroupRule_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "LoyaltyGroupRule",
                column: "LoyaltyProductGroupId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoyaltyGroupRule",
                schema: "loyalty");

            migrationBuilder.AddColumn<long>(
                name: "RuleId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LoyaltyProgramRule",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    IsArchived = table.Column<bool>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    RuleType = table.Column<int>(nullable: false),
                    RuleValue = table.Column<string>(nullable: true),
                    VenueId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyaltyProgramRule", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProductGroup_RuleId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                column: "RuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoyaltyProductGroup_LoyaltyProgramRule_RuleId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                column: "RuleId",
                principalSchema: "loyalty",
                principalTable: "LoyaltyProgramRule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
