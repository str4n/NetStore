using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetStore.Modules.Customers.Core.EF.Migrations
{
    /// <inheritdoc />
    public partial class Customers_Module_OrdersFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customers_CustomerId",
                schema: "customers",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLine_Order_OrderId",
                schema: "customers",
                table: "OrderLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                schema: "customers",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                schema: "customers",
                newName: "Orders",
                newSchema: "customers");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustomerId",
                schema: "customers",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                schema: "customers",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLine_Orders_OrderId",
                schema: "customers",
                table: "OrderLine",
                column: "OrderId",
                principalSchema: "customers",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                schema: "customers",
                table: "Orders",
                column: "CustomerId",
                principalSchema: "customers",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLine_Orders_OrderId",
                schema: "customers",
                table: "OrderLine");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                schema: "customers",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                schema: "customers",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                schema: "customers",
                newName: "Order",
                newSchema: "customers");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                schema: "customers",
                table: "Order",
                newName: "IX_Order_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                schema: "customers",
                table: "Order",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customers_CustomerId",
                schema: "customers",
                table: "Order",
                column: "CustomerId",
                principalSchema: "customers",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLine_Order_OrderId",
                schema: "customers",
                table: "OrderLine",
                column: "OrderId",
                principalSchema: "customers",
                principalTable: "Order",
                principalColumn: "Id");
        }
    }
}
