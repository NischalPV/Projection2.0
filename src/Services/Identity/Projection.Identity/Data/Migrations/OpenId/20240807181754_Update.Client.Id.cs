using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projection.Identity.Data.Migrations.OpenId
{
    /// <inheritdoc />
    public partial class UpdateClientId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "openid",
                table: "OpenIddictApplications",
                keyColumn: "Id",
                keyValue: "999b0131-f2f5-45ab-8b0b-5e24302ba119");

            migrationBuilder.DeleteData(
                schema: "openid",
                table: "OpenIddictApplications",
                keyColumn: "Id",
                keyValue: "cc9ee137-2548-4b6e-8a9e-3671b202f3e5");

            migrationBuilder.DeleteData(
                schema: "openid",
                table: "OpenIddictScopes",
                keyColumn: "Id",
                keyValue: "bd03e61a-137b-4a7b-bffe-eaba98e69c1b");

            migrationBuilder.InsertData(
                schema: "openid",
                table: "OpenIddictApplications",
                columns: new[] { "Id", "ApplicationType", "ClientId", "ClientSecret", "ClientType", "ConcurrencyToken", "ConsentType", "DisplayName", "DisplayNames", "JsonWebKeySet", "Permissions", "PostLogoutRedirectUris", "Properties", "RedirectUris", "Requirements", "Settings" },
                values: new object[,]
                {
                    { "01912e03-51a6-75cd-a1de-3aa4fb87a3d8", "web", "projection-tenants-api--dev", "bUE6r4ekrGN8HLmiko/LoLEC1KIiDyqwNtte4dwjrHY=", null, "34d13a1b-ada5-4bca-848b-b6b64c87106a", "implicit", "Projection Tenants API OpenId Client", null, null, "[\"ept:introspection\",\"ept:token\",\"ept:authorization\",\"ept:logout\",\"ept:device\",\"gt:implicit\",\"gt:authorization_code\",\"gt:refresh_token\",\"rst:code\",\"rst:id_token\",\"rst:token\",\"scp:email\",\"scp:profile\",\"scp:roles\",\"scp:projection-tenants-api\"]", "[\"https://localhost:7044/swagger/\"]", null, "[\"https://localhost:7044/swagger/oauth2-redirect.html\"]", "[\"ft:pkce\"]", null },
                    { "01912e03-51a6-7a70-bf92-4ae0ef632499", "web", "projection-ui--dev", "bUE6r4ekrGN8HLmiko/LoLEC1KIiDyqwNtte4dwjrHY=", null, "0954c756-76fe-410c-bd5b-b79fa223c2c4", "implicit", "Projection Frontend UI OpenId Client", null, null, "[\"ept:introspection\",\"ept:token\",\"ept:authorization\",\"ept:logout\",\"ept:device\",\"gt:implicit\",\"gt:authorization_code\",\"gt:refresh_token\",\"rst:code\",\"rst:id_token\",\"rst:token\",\"scp:email\",\"scp:profile\",\"scp:roles\",\"scp:projection-tenants-api\"]", "[\"http://localhost:6002/signout-callback-oidc\"]", null, "[\"http://localhost:6002/signin-oidc\"]", "[\"ft:pkce\"]", null }
                });

            migrationBuilder.InsertData(
                schema: "openid",
                table: "OpenIddictScopes",
                columns: new[] { "Id", "ConcurrencyToken", "Description", "Descriptions", "DisplayName", "DisplayNames", "Name", "Properties", "Resources" },
                values: new object[] { "01912e03-51a6-7892-9831-0f132b5417a2", "f32921aa-5ed0-41d5-8c92-8a26e4683b94", "This scope allows the client to access the Projection Tenants API", null, null, "{\"en-US\":\"Access to the Projection Tenants API\",\"fr-FR\":\"Accès à l'API des locataires de projection\",\"de-DE\":\"Zugriff auf die Projektionsmieter-API\"}", "projection-tenants-api", null, "[\"projection-tenants-api\"]" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "openid",
                table: "OpenIddictApplications",
                keyColumn: "Id",
                keyValue: "01912e03-51a6-75cd-a1de-3aa4fb87a3d8");

            migrationBuilder.DeleteData(
                schema: "openid",
                table: "OpenIddictApplications",
                keyColumn: "Id",
                keyValue: "01912e03-51a6-7a70-bf92-4ae0ef632499");

            migrationBuilder.DeleteData(
                schema: "openid",
                table: "OpenIddictScopes",
                keyColumn: "Id",
                keyValue: "01912e03-51a6-7892-9831-0f132b5417a2");

            migrationBuilder.InsertData(
                schema: "openid",
                table: "OpenIddictApplications",
                columns: new[] { "Id", "ApplicationType", "ClientId", "ClientSecret", "ClientType", "ConcurrencyToken", "ConsentType", "DisplayName", "DisplayNames", "JsonWebKeySet", "Permissions", "PostLogoutRedirectUris", "Properties", "RedirectUris", "Requirements", "Settings" },
                values: new object[,]
                {
                    { "999b0131-f2f5-45ab-8b0b-5e24302ba119", "web", "projection-ui--dev", "bUE6r4ekrGN8HLmiko/LoLEC1KIiDyqwNtte4dwjrHY=", null, "bb2f2cb2-9505-4b25-9b7d-2ce5c1d6f4c0", "implicit", "Projection Frontend UI OpenId Client", null, null, "[\"ept:introspection\",\"ept:token\",\"ept:authorization\",\"ept:logout\",\"ept:device\",\"gt:implicit\",\"gt:authorization_code\",\"gt:refresh_token\",\"rst:code\",\"rst:id_token\",\"rst:token\",\"scp:email\",\"scp:profile\",\"scp:roles\",\"scp:projection-tenants-api\"]", "[\"http://localhost:6002/signout-callback-oidc\"]", null, "[\"http://localhost:6002/signin-oidc\"]", "[\"ft:pkce\"]", null },
                    { "cc9ee137-2548-4b6e-8a9e-3671b202f3e5", "web", "projection-tenants-api--dev", "bUE6r4ekrGN8HLmiko/LoLEC1KIiDyqwNtte4dwjrHY=", "confidential", "a27fa057-864d-4f22-974d-52ad02e30ca1", "implicit", "Projection Tenants API OpenId Client", null, null, "[\"ept:introspection\",\"ept:token\",\"ept:authorization\",\"ept:logout\",\"ept:device\",\"gt:implicit\",\"gt:authorization_code\",\"gt:refresh_token\",\"rst:code\",\"rst:id_token\",\"rst:token\",\"scp:email\",\"scp:profile\",\"scp:roles\",\"scp:projection-tenants-api\"]", "[\"https://localhost:7044/swagger/\"]", null, "[\"https://localhost:7044/swagger/oauth2-redirect.html\"]", "[\"ft:pkce\"]", null }
                });

            migrationBuilder.InsertData(
                schema: "openid",
                table: "OpenIddictScopes",
                columns: new[] { "Id", "ConcurrencyToken", "Description", "Descriptions", "DisplayName", "DisplayNames", "Name", "Properties", "Resources" },
                values: new object[] { "bd03e61a-137b-4a7b-bffe-eaba98e69c1b", "df4726c0-3326-44c2-9cdd-6500bb6b0526", null, null, null, "{\"en-US\":\"Access to the Projection Tenants API\",\"fr-FR\":\"Accès à l'API des locataires de projection\",\"de-DE\":\"Zugriff auf die Projektionsmieter-API\"}", "projection-tenants-api", null, "[\"projection-tenants-api\"]" });
        }
    }
}
