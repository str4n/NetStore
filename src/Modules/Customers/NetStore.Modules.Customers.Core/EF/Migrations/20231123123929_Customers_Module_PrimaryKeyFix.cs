using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetStore.Modules.Customers.Core.EF.Migrations
{
    /// <inheritdoc />
    public partial class Customers_Module_PrimaryKeyFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                schema: "customers",
                table: "Addresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                schema: "customers",
                table: "Addresses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerId",
                schema: "customers",
                table: "Addresses",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                schema: "customers",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CustomerId",
                schema: "customers",
                table: "Addresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                schema: "customers",
                table: "Addresses",
                column: "CustomerId");
        }
    }
}
