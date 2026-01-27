using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class AddedUserCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCode",
                schema: "loyalty",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    CodeValue = table.Column<string>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCode", x => x.UserId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCode_CodeValue",
                schema: "loyalty",
                table: "UserCode",
                column: "CodeValue",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCode",
                schema: "loyalty");
        }
    }
}
