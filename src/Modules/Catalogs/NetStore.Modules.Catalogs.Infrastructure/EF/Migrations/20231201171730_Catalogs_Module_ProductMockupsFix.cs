using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NetStore.Modules.Catalogs.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Catalogs_Module_ProductMockupsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Brands_BrandId",
                schema: "catalogs",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_CategoryId",
                schema: "catalogs",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                schema: "catalogs",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                schema: "catalogs",
                newName: "Products",
                newSchema: "catalogs");

            migrationBuilder.RenameIndex(
                name: "IX_Product_Name",
                schema: "catalogs",
                table: "Products",
                newName: "IX_Products_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryId",
                schema: "catalogs",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_BrandId",
                schema: "catalogs",
                table: "Products",
                newName: "IX_Products_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                schema: "catalogs",
                table: "Products",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductMockups",
                schema: "catalogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    BrandId = table.Column<long>(type: "bigint", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Fabric = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMockups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductMockups_Brands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "catalogs",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductMockups_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "catalogs",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductMockups_BrandId",
                schema: "catalogs",
                table: "ProductMockups",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMockups_CategoryId",
                schema: "catalogs",
                table: "ProductMockups",
                column: "CategoryId");

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

            migrationBuilder.DropTable(
                name: "ProductMockups",
                schema: "catalogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                schema: "catalogs",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "catalogs",
                newName: "Product",
                newSchema: "catalogs");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Name",
                schema: "catalogs",
                table: "Product",
                newName: "IX_Product_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                schema: "catalogs",
                table: "Product",
                newName: "IX_Product_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandId",
                schema: "catalogs",
                table: "Product",
                newName: "IX_Product_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                schema: "catalogs",
                table: "Product",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Brands_BrandId",
                schema: "catalogs",
                table: "Product",
                column: "BrandId",
                principalSchema: "catalogs",
                principalTable: "Brands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categories_CategoryId",
                schema: "catalogs",
                table: "Product",
                column: "CategoryId",
                principalSchema: "catalogs",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
