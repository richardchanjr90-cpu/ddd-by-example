using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "loyalty",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalUid",
                schema: "loyalty",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUri",
                schema: "loyalty",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailableForOrder",
                schema: "loyalty",
                table: "Product",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "loyalty",
                table: "Product",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    VenueId = table.Column<long>(nullable: false),
                    PlacedDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    PickUpTime = table.Column<DateTime>(nullable: true),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    OrderId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "loyalty",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "loyalty",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                schema: "loyalty",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                schema: "loyalty",
                table: "OrderItem",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "loyalty");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ExternalUid",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ImageUri",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IsAvailableForOrder",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "loyalty",
                table: "Product");
        }
    }
}
