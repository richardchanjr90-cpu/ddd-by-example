using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class Product_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailableForOrder",
                schema: "loyalty",
                table: "ProductGroup");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "loyalty",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailableForOrder",
                schema: "loyalty",
                table: "ProductGroup",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
