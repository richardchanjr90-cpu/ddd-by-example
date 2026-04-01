using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalUid",
                schema: "loyalty",
                table: "ProductGroup",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUri",
                schema: "loyalty",
                table: "ProductGroup",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                schema: "loyalty",
                table: "ProductGroup",
                nullable: false,
                defaultValue: false);

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
                name: "IsAvailable",
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
                name: "VenueMenu",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    VenueId = table.Column<long>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VenueMenu_Venue_VenueId",
                        column: x => x.VenueId,
                        principalSchema: "loyalty",
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    VenueId = table.Column<long>(nullable: false),
                    MenuId = table.Column<long>(nullable: false),
                    PlacedDate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    PickUpTime = table.Column<DateTime>(nullable: true),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_VenueMenu_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "loyalty",
                        principalTable: "VenueMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VenueMenuProductGroup",
                schema: "loyalty",
                columns: table => new
                {
                    MenuId = table.Column<long>(nullable: false),
                    ProductGroupId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueMenuProductGroup", x => new { x.MenuId, x.ProductGroupId });
                    table.ForeignKey(
                        name: "FK_VenueMenuProductGroup_ProductGroup_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "loyalty",
                        principalTable: "ProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VenueMenuProductGroup_VenueMenu_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalSchema: "loyalty",
                        principalTable: "VenueMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
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
                name: "IX_Order_MenuId",
                schema: "loyalty",
                table: "Order",
                column: "MenuId");

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

            migrationBuilder.CreateIndex(
                name: "IX_VenueMenu_VenueId",
                schema: "loyalty",
                table: "VenueMenu",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_VenueMenuProductGroup_ProductGroupId",
                schema: "loyalty",
                table: "VenueMenuProductGroup",
                column: "ProductGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "VenueMenuProductGroup",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "VenueMenu",
                schema: "loyalty");

            migrationBuilder.DropColumn(
                name: "ExternalUid",
                schema: "loyalty",
                table: "ProductGroup");

            migrationBuilder.DropColumn(
                name: "ImageUri",
                schema: "loyalty",
                table: "ProductGroup");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                schema: "loyalty",
                table: "ProductGroup");

            migrationBuilder.DropColumn(
                name: "ExternalUid",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ImageUri",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                schema: "loyalty",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "loyalty",
                table: "Product");
        }
    }
}
