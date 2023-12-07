using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetStore.Modules.Orders.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Orders_Module_Cart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                schema: "orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CheckoutCart",
                schema: "orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Payment = table.Column<string>(type: "text", nullable: true),
                    Shipment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutCart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartProduct",
                schema: "orders",
                columns: table => new
                {
                    CartId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    CheckoutCartId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProduct", x => new { x.CartId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CartProduct_Carts_CartId",
                        column: x => x.CartId,
                        principalSchema: "orders",
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProduct_CheckoutCart_CheckoutCartId",
                        column: x => x.CheckoutCartId,
                        principalSchema: "orders",
                        principalTable: "CheckoutCart",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "orders",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartProduct_CheckoutCartId",
                schema: "orders",
                table: "CartProduct",
                column: "CheckoutCartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProduct_ProductId",
                schema: "orders",
                table: "CartProduct",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartProduct",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "Carts",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "CheckoutCart",
                schema: "orders");
        }
    }
}
