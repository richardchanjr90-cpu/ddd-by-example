using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "loyalty");

            migrationBuilder.CreateSequence(
                name: "producteq",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "productgroupeq",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "venueeq",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "workereq",
                incrementBy: 10);

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
                    Comment = table.Column<string>(nullable: true),
                    VenueComment = table.Column<string>(nullable: true),
                    Rate = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Venue",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    OwnerId = table.Column<string>(nullable: false),
                    ParentId = table.Column<long>(nullable: true),
                    Location_City = table.Column<string>(maxLength: 200, nullable: true),
                    Location_Address = table.Column<string>(maxLength: 200, nullable: true),
                    Location_Latitude = table.Column<float>(nullable: true),
                    Location_Longitude = table.Column<float>(nullable: true),
                    Details_FullDescription = table.Column<string>(maxLength: 4000, nullable: true),
                    Details_Description = table.Column<string>(maxLength: 2000, nullable: true),
                    Details_WorkingHours = table.Column<string>(nullable: true),
                    ContactInfo_Phones = table.Column<string>(nullable: true),
                    ContactInfo_WebSites = table.Column<string>(nullable: true),
                    ContactInfo_Instagram = table.Column<string>(nullable: true),
                    ContactInfo_Facebook = table.Column<string>(nullable: true),
                    ContactInfo_Vkontakte = table.Column<string>(nullable: true),
                    LogoUrl = table.Column<string>(maxLength: 200, nullable: true),
                    Images = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    CategoryType = table.Column<long>(nullable: false),
                    VenueStatus = table.Column<int>(nullable: false),
                    AcceptsOrders = table.Column<bool>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Worker",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    WorkerId = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhotoUri = table.Column<string>(nullable: true),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoyaltyProgram",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    VenueId = table.Column<long>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyaltyProgram", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoyaltyProgram_Venue_VenueId",
                        column: x => x.VenueId,
                        principalSchema: "loyalty",
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Icon = table.Column<int>(nullable: true),
                    ImageUri = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    IsAvailableForOrder = table.Column<bool>(nullable: false),
                    ExternalUid = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsArchived = table.Column<bool>(nullable: false),
                    ProductGroupId = table.Column<long>(nullable: false),
                    VenueId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Venue_VenueId",
                        column: x => x.VenueId,
                        principalSchema: "loyalty",
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductGroup",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    VenueId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Icon = table.Column<int>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductGroup_Venue_VenueId",
                        column: x => x.VenueId,
                        principalSchema: "loyalty",
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VenueWorker",
                schema: "loyalty",
                columns: table => new
                {
                    VenueId = table.Column<long>(nullable: false),
                    WorkerId = table.Column<long>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    PositionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueWorker", x => new { x.VenueId, x.WorkerId });
                    table.ForeignKey(
                        name: "FK_VenueWorker_Worker_VenueId",
                        column: x => x.VenueId,
                        principalSchema: "loyalty",
                        principalTable: "Worker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "LoyaltyProductGroup",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LoyaltyProgramId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProductGroupId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    IsArchived = table.Column<bool>(nullable: false)
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
                    table.ForeignKey(
                        name: "FK_LoyaltyProductGroup_ProductGroup_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalSchema: "loyalty",
                        principalTable: "ProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoyaltyGroupRule",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LoyaltyProductGroupId = table.Column<long>(nullable: false),
                    RuleType = table.Column<int>(nullable: false),
                    Rule = table.Column<string>(nullable: true),
                    RuleVersion = table.Column<int>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Purchase",
                schema: "loyalty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LoyaltyProductGroupId = table.Column<long>(nullable: false),
                    VenueId = table.Column<long>(nullable: false),
                    ProductId = table.Column<long>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Value = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchase_LoyaltyProductGroup_LoyaltyProductGroupId",
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
                column: "LoyaltyProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProductGroup_ProductGroupId",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProductGroup_LoyaltyProgramId_Name",
                schema: "loyalty",
                table: "LoyaltyProductGroup",
                columns: new[] { "LoyaltyProgramId", "Name" },
                unique: true,
                filter: "[IsArchived] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProgram_VenueId_Name",
                schema: "loyalty",
                table: "LoyaltyProgram",
                columns: new[] { "VenueId", "Name" },
                unique: true,
                filter: "[IsArchived] = 0");

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
                name: "IX_Product_VenueId",
                schema: "loyalty",
                table: "Product",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductGroupId_Name",
                schema: "loyalty",
                table: "Product",
                columns: new[] { "ProductGroupId", "Name" },
                unique: true,
                filter: "[IsArchived] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_VenueId_Name",
                schema: "loyalty",
                table: "ProductGroup",
                columns: new[] { "VenueId", "Name" },
                unique: true,
                filter: "[IsArchived] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_LoyaltyProductGroupId",
                schema: "loyalty",
                table: "Purchase",
                column: "LoyaltyProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCode_CodeValue",
                schema: "loyalty",
                table: "UserCode",
                column: "CodeValue",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VenueWorker_VenueId_WorkerId",
                schema: "loyalty",
                table: "VenueWorker",
                columns: new[] { "VenueId", "WorkerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Worker_Email",
                schema: "loyalty",
                table: "Worker",
                column: "Email",
                unique: true,
                filter: "([Email] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_Phone",
                schema: "loyalty",
                table: "Worker",
                column: "Phone",
                unique: true,
                filter: "[Phone] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_WorkerId",
                schema: "loyalty",
                table: "Worker",
                column: "WorkerId",
                unique: true,
                filter: "([IsArchived] = 0 AND WorkerId IS NOT NULL)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoyaltyGroupRule",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "OrderItem",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "Purchase",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "UserCode",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "VenueWorker",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "LoyaltyProductGroup",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "Worker",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "LoyaltyProgram",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "ProductGroup",
                schema: "loyalty");

            migrationBuilder.DropTable(
                name: "Venue",
                schema: "loyalty");

            migrationBuilder.DropSequence(
                name: "producteq");

            migrationBuilder.DropSequence(
                name: "productgroupeq");

            migrationBuilder.DropSequence(
                name: "venueeq");

            migrationBuilder.DropSequence(
                name: "workereq");
        }
    }
}
