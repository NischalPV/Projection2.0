using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projection.Identity.Data.Migrations.OpenId
{
    /// <inheritdoc />
    public partial class AddClientTenantAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "openid",
                table: "OpenIddictApplications",
                keyColumn: "Id",
                keyValue: "732fddb5-1ccf-416b-b99c-5c674b62a877");

            migrationBuilder.DeleteData(
                schema: "openid",
                table: "OpenIddictScopes",
                keyColumn: "Id",
                keyValue: "577f8b7c-0b07-4bc5-8d6c-40009235569e");

            migrationBuilder.InsertData(
                schema: "openid",
                table: "OpenIddictApplications",
                columns: new[] { "Id", "ApplicationType", "ClientId", "ClientSecret", "ClientType", "ConcurrencyToken", "ConsentType", "DisplayName", "DisplayNames", "JsonWebKeySet", "Permissions", "PostLogoutRedirectUris", "Properties", "RedirectUris", "Requirements", "Settings" },
                values: new object[,]
                {
                    { "08819e97-0beb-4daa-8f10-afd4788f5299", "web", "projection-tenants-api--dev", "bUE6r4ekrGN8HLmiko/LoLEC1KIiDyqwNtte4dwjrHY=", "confidential", "0859b98d-30fe-4ce5-8333-6b99422331db", "implicit", "Projection Tenants API OpenId Client", null, null, "[\"ept:authorization\",\"ept:logout\",\"ept:token\",\"ept:introspection\",\"ept:device\",\"gt:authorization_code\",\"gt:refresh_token\",\"gt:client_credentials\",\"rst:code id_token token\",\"scp:email\",\"scp:profile\",\"scp:roles\",\"scp:projection-tenants-api\"]", "[\"http://localhost:6002/swagger/\"]", null, "[\"http://localhost:6002/swagger/oauth2-redirect.html\"]", "[\"ft:pkce\"]", null },
                    { "956b723f-f275-4f56-9859-61736320fb05", "web", "projection-ui--dev", "bUE6r4ekrGN8HLmiko/LoLEC1KIiDyqwNtte4dwjrHY=", null, "e4897c37-2acf-4e66-a825-0206626d840a", "implicit", "Projection Frontend UI OpenId Client", null, null, "[\"ept:authorization\",\"ept:logout\",\"ept:token\",\"ept:introspection\",\"ept:device\",\"gt:authorization_code\",\"gt:refresh_token\",\"gt:implicit\",\"rst:code id_token token\",\"scp:email\",\"scp:profile\",\"scp:roles\",\"scp:projection-tenants-api\"]", "[\"http://localhost:6002/signout-callback-oidc\"]", null, "[\"http://localhost:6002/signin-oidc\"]", "[\"ft:pkce\"]", null }
                });

            migrationBuilder.InsertData(
                schema: "openid",
                table: "OpenIddictScopes",
                columns: new[] { "Id", "ConcurrencyToken", "Description", "Descriptions", "DisplayName", "DisplayNames", "Name", "Properties", "Resources" },
                values: new object[] { "414f06fe-ddec-4d07-ab37-4711e5aa7ae0", "5841795e-884d-4025-b9f2-eedeb3464ccc", null, null, null, "{\"en-US\":\"Access to the Projection Tenants API\",\"fr-FR\":\"Accès à l'API des locataires de projection\",\"de-DE\":\"Zugriff auf die Projektionsmieter-API\"}", "projection-tenants-api", null, "[\"projection-tenants-api\"]" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "openid",
                table: "OpenIddictApplications",
                keyColumn: "Id",
                keyValue: "08819e97-0beb-4daa-8f10-afd4788f5299");

            migrationBuilder.DeleteData(
                schema: "openid",
                table: "OpenIddictApplications",
                keyColumn: "Id",
                keyValue: "956b723f-f275-4f56-9859-61736320fb05");

            migrationBuilder.DeleteData(
                schema: "openid",
                table: "OpenIddictScopes",
                keyColumn: "Id",
                keyValue: "414f06fe-ddec-4d07-ab37-4711e5aa7ae0");

            migrationBuilder.InsertData(
                schema: "openid",
                table: "OpenIddictApplications",
                columns: new[] { "Id", "ApplicationType", "ClientId", "ClientSecret", "ClientType", "ConcurrencyToken", "ConsentType", "DisplayName", "DisplayNames", "JsonWebKeySet", "Permissions", "PostLogoutRedirectUris", "Properties", "RedirectUris", "Requirements", "Settings" },
                values: new object[] { "732fddb5-1ccf-416b-b99c-5c674b62a877", "web", "projection-ui--dev", "bUE6r4ekrGN8HLmiko/LoLEC1KIiDyqwNtte4dwjrHY=", "public", "aee2c88e-c5c7-4d5b-b2d2-60df00fd291d", "implicit", "Projection Frontend UI OpenId Client", null, null, "[\"ept:authorization\",\"ept:logout\",\"ept:token\",\"ept:introspection\",\"ept:device\",\"gt:authorization_code\",\"gt:refresh_token\",\"gt:implicit\",\"rst:code id_token token\",\"scp:email\",\"scp:profile\",\"scp:roles\",\"scp:projection-tenants-api\"]", "[\"http://localhost:6002/swagger/\"]", null, "[\"http://localhost:6002/swagger/oauth2-redirect.html\"]", "[\"ft:pkce\"]", null });

            migrationBuilder.InsertData(
                schema: "openid",
                table: "OpenIddictScopes",
                columns: new[] { "Id", "ConcurrencyToken", "Description", "Descriptions", "DisplayName", "DisplayNames", "Name", "Properties", "Resources" },
                values: new object[] { "577f8b7c-0b07-4bc5-8d6c-40009235569e", "3bcf76e8-96f5-4abe-99bf-552b019795f8", null, null, null, "{\"en-US\":\"Access to the Projection Tenants API\",\"fr-FR\":\"Accès à l'API des locataires de projection\",\"de-DE\":\"Zugriff auf die Projektionsmieter-API\"}", "projection-tenants-api", null, "[\"projection-tenants-api\"]" });
        }
    }
}
