﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetStore.Modules.Customers.Core.EF.Migrations
{
    /// <inheritdoc />
    public partial class Customers_Module_CustomerStatusIntroducted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                schema: "customers",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                schema: "customers",
                table: "Customers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
