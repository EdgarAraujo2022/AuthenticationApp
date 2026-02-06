using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenticationApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameTablesToLowercase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientScopes_Clients_ClientId",
                schema: "auth",
                table: "ClientScopes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                schema: "auth",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientScopes",
                schema: "auth",
                table: "ClientScopes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                schema: "auth",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "auth",
                newName: "users",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "ClientScopes",
                schema: "auth",
                newName: "clientScopes",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "Clients",
                schema: "auth",
                newName: "clients",
                newSchema: "auth");

            migrationBuilder.RenameIndex(
                name: "IX_ClientScopes_ClientId",
                schema: "auth",
                table: "clientScopes",
                newName: "IX_clientScopes_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                schema: "auth",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clientScopes",
                schema: "auth",
                table: "clientScopes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clients",
                schema: "auth",
                table: "clients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_clientScopes_clients_ClientId",
                schema: "auth",
                table: "clientScopes",
                column: "ClientId",
                principalSchema: "auth",
                principalTable: "clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clientScopes_clients_ClientId",
                schema: "auth",
                table: "clientScopes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                schema: "auth",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clientScopes",
                schema: "auth",
                table: "clientScopes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clients",
                schema: "auth",
                table: "clients");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "auth",
                newName: "Users",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "clientScopes",
                schema: "auth",
                newName: "ClientScopes",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "clients",
                schema: "auth",
                newName: "Clients",
                newSchema: "auth");

            migrationBuilder.RenameIndex(
                name: "IX_clientScopes_ClientId",
                schema: "auth",
                table: "ClientScopes",
                newName: "IX_ClientScopes_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                schema: "auth",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientScopes",
                schema: "auth",
                table: "ClientScopes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                schema: "auth",
                table: "Clients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientScopes_Clients_ClientId",
                schema: "auth",
                table: "ClientScopes",
                column: "ClientId",
                principalSchema: "auth",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
