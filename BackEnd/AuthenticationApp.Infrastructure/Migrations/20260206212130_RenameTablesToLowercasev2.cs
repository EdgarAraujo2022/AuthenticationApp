using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenticationApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameTablesToLowercasev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clientScopes_clients_ClientId",
                schema: "auth",
                table: "clientScopes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clientScopes",
                schema: "auth",
                table: "clientScopes");

            migrationBuilder.RenameTable(
                name: "clientScopes",
                schema: "auth",
                newName: "client_scopes",
                newSchema: "auth");

            migrationBuilder.RenameColumn(
                name: "Username",
                schema: "auth",
                table: "users",
                newName: "user_name");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "auth",
                table: "users",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                schema: "auth",
                table: "users",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "RefreshTokens",
                newName: "token");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "RefreshTokens",
                newName: "client_id");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "auth",
                table: "clients",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "ClientSecret",
                schema: "auth",
                table: "clients",
                newName: "client_secret");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                schema: "auth",
                table: "clients",
                newName: "client_id");

            migrationBuilder.RenameColumn(
                name: "Scope",
                schema: "auth",
                table: "client_scopes",
                newName: "scope");

            migrationBuilder.RenameIndex(
                name: "IX_clientScopes_ClientId",
                schema: "auth",
                table: "client_scopes",
                newName: "IX_client_scopes_ClientId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "auth",
                table: "users",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "auth",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "auth",
                table: "clients",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "auth",
                table: "client_scopes",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId1",
                schema: "auth",
                table: "client_scopes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_client_scopes",
                schema: "auth",
                table: "client_scopes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_client_id",
                table: "RefreshTokens",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_clients_client_id",
                schema: "auth",
                table: "clients",
                column: "client_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_client_scopes_ClientId1",
                schema: "auth",
                table: "client_scopes",
                column: "ClientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_client_scopes_clients_ClientId",
                schema: "auth",
                table: "client_scopes",
                column: "ClientId",
                principalSchema: "auth",
                principalTable: "clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_client_scopes_clients_ClientId1",
                schema: "auth",
                table: "client_scopes",
                column: "ClientId1",
                principalSchema: "auth",
                principalTable: "clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_clients_client_id",
                table: "RefreshTokens",
                column: "client_id",
                principalSchema: "auth",
                principalTable: "clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_scopes_clients_ClientId",
                schema: "auth",
                table: "client_scopes");

            migrationBuilder.DropForeignKey(
                name: "FK_client_scopes_clients_ClientId1",
                schema: "auth",
                table: "client_scopes");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_clients_client_id",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_client_id",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_clients_client_id",
                schema: "auth",
                table: "clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_client_scopes",
                schema: "auth",
                table: "client_scopes");

            migrationBuilder.DropIndex(
                name: "IX_client_scopes_ClientId1",
                schema: "auth",
                table: "client_scopes");

            migrationBuilder.DropColumn(
                name: "ClientId1",
                schema: "auth",
                table: "client_scopes");

            migrationBuilder.RenameTable(
                name: "client_scopes",
                schema: "auth",
                newName: "clientScopes",
                newSchema: "auth");

            migrationBuilder.RenameColumn(
                name: "user_name",
                schema: "auth",
                table: "users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "created_at",
                schema: "auth",
                table: "users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                schema: "auth",
                table: "users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "token",
                table: "RefreshTokens",
                newName: "Token");

            migrationBuilder.RenameColumn(
                name: "client_id",
                table: "RefreshTokens",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "is_active",
                schema: "auth",
                table: "clients",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "client_secret",
                schema: "auth",
                table: "clients",
                newName: "ClientSecret");

            migrationBuilder.RenameColumn(
                name: "client_id",
                schema: "auth",
                table: "clients",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "scope",
                schema: "auth",
                table: "clientScopes",
                newName: "Scope");

            migrationBuilder.RenameIndex(
                name: "IX_client_scopes_ClientId",
                schema: "auth",
                table: "clientScopes",
                newName: "IX_clientScopes_ClientId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "auth",
                table: "users",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "auth",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "auth",
                table: "clients",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "auth",
                table: "clientScopes",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clientScopes",
                schema: "auth",
                table: "clientScopes",
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
    }
}
