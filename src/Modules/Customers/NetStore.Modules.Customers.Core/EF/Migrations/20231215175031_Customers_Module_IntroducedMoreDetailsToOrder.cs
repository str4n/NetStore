using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetStore.Modules.Customers.Core.EF.Migrations
{
    /// <inheritdoc />
    public partial class Customers_Module_IntroducedMoreDetailsToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                schema: "customers",
                table: "Order",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_PostalCode",
                schema: "customers",
                table: "Order",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                schema: "customers",
                table: "Order",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PlaceDate",
                schema: "customers",
                table: "Order",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ReceiverName",
                schema: "customers",
                table: "Order",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "customers",
                table: "Order",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                schema: "customers",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Address_PostalCode",
                schema: "customers",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                schema: "customers",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "PlaceDate",
                schema: "customers",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ReceiverName",
                schema: "customers",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "customers",
                table: "Order");
        }
    }
}
