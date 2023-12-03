using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetStore.Modules.Catalogs.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Catalogs_Module_ProductWeightFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                schema: "catalogs",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                schema: "catalogs",
                table: "Products");

            migrationBuilder.AlterColumn<long>(
                name: "CategoryId",
                schema: "catalogs",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BrandId",
                schema: "catalogs",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                schema: "catalogs",
                table: "Products",
                column: "BrandId",
                principalSchema: "catalogs",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                schema: "catalogs",
                table: "Products",
                column: "CategoryId",
                principalSchema: "catalogs",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                schema: "catalogs",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                schema: "catalogs",
                table: "Products");

            migrationBuilder.AlterColumn<long>(
                name: "CategoryId",
                schema: "catalogs",
                table: "Products",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "BrandId",
                schema: "catalogs",
                table: "Products",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                schema: "catalogs",
                table: "Products",
                column: "BrandId",
                principalSchema: "catalogs",
                principalTable: "Brands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                schema: "catalogs",
                table: "Products",
                column: "CategoryId",
                principalSchema: "catalogs",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
