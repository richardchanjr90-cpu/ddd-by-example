using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class Updated_Orders2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalUid",
                schema: "loyalty",
                table: "ProductGroup");

            migrationBuilder.DropColumn(
                name: "ImageUri",
                schema: "loyalty",
                table: "ProductGroup");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalUid",
                schema: "loyalty",
                table: "ProductGroup",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUri",
                schema: "loyalty",
                table: "ProductGroup",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
