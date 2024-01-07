using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetStore.Modules.Customers.Core.EF.Migrations
{
    /// <inheritdoc />
    public partial class Customers_Module_IntroducedProductIdToOrderline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                schema: "customers",
                table: "Orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                schema: "customers",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                schema: "customers",
                table: "OrderLine",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                schema: "customers",
                table: "Orders",
                column: "CustomerId",
                principalSchema: "customers",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                schema: "customers",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "customers",
                table: "OrderLine");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                schema: "customers",
                table: "Orders",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                schema: "customers",
                table: "Orders",
                column: "CustomerId",
                principalSchema: "customers",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
