using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetStore.Modules.Orders.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Orders_Module_CodeNameRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeName",
                schema: "orders",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeName",
                schema: "orders",
                table: "Products",
                type: "text",
                nullable: true);
        }
    }
}
