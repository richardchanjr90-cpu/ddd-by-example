using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class Added_Loyalty_Rule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Type",
                schema: "loyalty",
                table: "LoyaltyProductGroup");

            migrationBuilder.DropColumn(
                "BusinessRule",
                schema: "loyalty",
                table: "LoyaltyProduct");

            migrationBuilder.AlterColumn<string>(
                "LogoUrl",
                schema: "loyalty",
                table: "Venue",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                "LoyaltyProgramRule",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    RuleType = table.Column<int>(nullable: false),
                    LoyaltyProgram = table.Column<long>(nullable: true),
                    RuleValue = table.Column<string>(nullable: true),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyaltyProgramRule", x => x.Id);
                    table.ForeignKey(
                        "FK_LoyaltyProgramRule_LoyaltyProgram_LoyaltyProgram",
                        x => x.LoyaltyProgram,
                        principalSchema: "loyalty",
                        principalTable: "LoyaltyProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_LoyaltyProgramRule_LoyaltyProgram",
                schema: "loyalty",
                table: "LoyaltyProgramRule",
                column: "LoyaltyProgram",
                unique: true,
                filter: "[LoyaltyProgram] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "LoyaltyProgramRule",
                "loyalty");

            migrationBuilder.AlterColumn<string>(
                "LogoUrl",
                schema: "loyalty",
                table: "Venue",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                "Type",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                "BusinessRule",
                schema: "loyalty",
                table: "LoyaltyProduct",
                nullable: false,
                defaultValue: "");
        }
    }
}
