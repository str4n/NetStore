using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NetStore.Modules.Catalogs.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Catalogs_Module_Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "catalogs");

            migrationBuilder.CreateTable(
                name: "Brands",
                schema: "catalogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "catalogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "catalogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: true),
                    BrandId = table.Column<long>(type: "bigint", nullable: true),
                    Model = table.Column<string>(type: "text", nullable: false),
                    NetPrice = table.Column<double>(type: "double precision", nullable: false),
                    GrossPrice = table.Column<double>(type: "double precision", nullable: false),
                    Fabric = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    AgeCategory = table.Column<int>(type: "integer", nullable: false),
                    Size = table.Column<int>(type: "integer", nullable: false),
                    Color = table.Column<int>(type: "integer", nullable: false),
                    SKU = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Brands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "catalogs",
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "catalogs",
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId",
                schema: "catalogs",
                table: "Product",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                schema: "catalogs",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Name",
                schema: "catalogs",
                table: "Product",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product",
                schema: "catalogs");

            migrationBuilder.DropTable(
                name: "Brands",
                schema: "catalogs");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "catalogs");
        }
    }
}
