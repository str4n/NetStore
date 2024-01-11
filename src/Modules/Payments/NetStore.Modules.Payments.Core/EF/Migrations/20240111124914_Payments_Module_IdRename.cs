using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetStore.Modules.Payments.Core.EF.Migrations
{
    /// <inheritdoc />
    public partial class Payments_Module_IdRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                schema: "payments",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "payments",
                table: "Payments",
                newName: "CustomerId");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                schema: "payments",
                table: "Payments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                schema: "payments",
                table: "Payments",
                column: "PaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                schema: "payments",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                schema: "payments",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                schema: "payments",
                table: "Payments",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                schema: "payments",
                table: "Payments",
                column: "Id");
        }
    }
}
