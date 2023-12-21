using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetStore.Modules.Orders.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Orders_Module_ProductRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                schema: "orders",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                schema: "orders",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                schema: "orders",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "State",
                schema: "orders",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
