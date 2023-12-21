using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NetStore.Modules.Catalogs.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Catalogs_Module_ProductRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductMockups",
                schema: "catalogs");

            migrationBuilder.DropColumn(
                name: "CodeName",
                schema: "catalogs",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                schema: "catalogs",
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
                schema: "catalogs",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "CodeName",
                schema: "catalogs",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductMockups",
                schema: "catalogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrandId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Fabric = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
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
        }
    }
}
