using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetStore.Modules.Orders.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Orders_Module_OrderIntroduced : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProduct_CheckoutCart_CheckoutCartId",
                schema: "orders",
                table: "CartProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckoutCart",
                schema: "orders",
                table: "CheckoutCart");

            migrationBuilder.RenameTable(
                name: "CheckoutCart",
                schema: "orders",
                newName: "CheckoutCarts",
                newSchema: "orders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckoutCarts",
                schema: "orders",
                table: "CheckoutCarts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Payment = table.Column<string>(type: "text", nullable: true),
                    Shipment = table.Column<string>(type: "text", nullable: true),
                    PlaceDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderLine",
                schema: "orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderLineNumber = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    SKU = table.Column<string>(type: "text", nullable: true),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLine_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "orders",
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_OrderId",
                schema: "orders",
                table: "OrderLine",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProduct_CheckoutCarts_CheckoutCartId",
                schema: "orders",
                table: "CartProduct",
                column: "CheckoutCartId",
                principalSchema: "orders",
                principalTable: "CheckoutCarts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProduct_CheckoutCarts_CheckoutCartId",
                schema: "orders",
                table: "CartProduct");

            migrationBuilder.DropTable(
                name: "OrderLine",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckoutCarts",
                schema: "orders",
                table: "CheckoutCarts");

            migrationBuilder.RenameTable(
                name: "CheckoutCarts",
                schema: "orders",
                newName: "CheckoutCart",
                newSchema: "orders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckoutCart",
                schema: "orders",
                table: "CheckoutCart",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProduct_CheckoutCart_CheckoutCartId",
                schema: "orders",
                table: "CartProduct",
                column: "CheckoutCartId",
                principalSchema: "orders",
                principalTable: "CheckoutCart",
                principalColumn: "Id");
        }
    }
}
