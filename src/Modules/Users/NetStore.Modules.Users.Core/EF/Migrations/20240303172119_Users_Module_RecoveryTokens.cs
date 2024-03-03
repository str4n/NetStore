using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetStore.Modules.Users.Core.EF.Migrations
{
    /// <inheritdoc />
    public partial class Users_Module_RecoveryTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Secret",
                schema: "users",
                table: "ActivationTokens",
                newName: "Token");

            migrationBuilder.CreateTable(
                name: "RecoveryTokens",
                schema: "users",
                columns: table => new
                {
                    Token = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecoveryTokens", x => x.Token);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecoveryTokens",
                schema: "users");

            migrationBuilder.RenameColumn(
                name: "Token",
                schema: "users",
                table: "ActivationTokens",
                newName: "Secret");
        }
    }
}
