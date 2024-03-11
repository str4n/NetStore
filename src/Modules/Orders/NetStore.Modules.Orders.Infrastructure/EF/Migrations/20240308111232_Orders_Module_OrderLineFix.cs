using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetStore.Modules.Orders.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Orders_Module_OrderLineFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SKU",
                schema: "orders",
                table: "OrderLine",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "orders",
                table: "OrderLine",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                schema: "orders",
                table: "OrderLine",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "UnitPrice",
                schema: "orders",
                table: "OrderLine",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "orders",
                table: "OrderLine");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                schema: "orders",
                table: "OrderLine");

            migrationBuilder.AlterColumn<string>(
                name: "SKU",
                schema: "orders",
                table: "OrderLine",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "orders",
                table: "OrderLine",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
