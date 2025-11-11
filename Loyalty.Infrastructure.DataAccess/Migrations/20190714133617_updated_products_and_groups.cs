using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class updated_products_and_groups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Icon",
                schema: "loyalty",
                table: "ProductGroup",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "Icon",
                schema: "loyalty",
                table: "Product",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Icon",
                schema: "loyalty",
                table: "ProductGroup",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Icon",
                schema: "loyalty",
                table: "Product",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
