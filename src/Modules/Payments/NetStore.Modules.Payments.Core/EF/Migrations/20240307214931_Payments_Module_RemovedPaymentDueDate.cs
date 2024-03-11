using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetStore.Modules.Payments.Core.EF.Migrations
{
    /// <inheritdoc />
    public partial class Payments_Module_RemovedPaymentDueDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                schema: "payments",
                table: "Payments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                schema: "payments",
                table: "Payments",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
