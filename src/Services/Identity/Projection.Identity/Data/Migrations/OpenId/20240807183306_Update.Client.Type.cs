using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projection.Identity.Data.Migrations.OpenId
{
    /// <inheritdoc />
    public partial class UpdateClientType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "openid",
                table: "OpenIddictApplications",
                keyColumn: "Id",
                keyValue: "01912e03-51a6-75cd-a1de-3aa4fb87a3d8",
                columns: new[] { "ApplicationType", "ConcurrencyToken" },
                values: new object[] { null, "e0622ff9-1a64-4d8b-aa21-c0b14cbbec01" });

            migrationBuilder.UpdateData(
                schema: "openid",
                table: "OpenIddictApplications",
                keyColumn: "Id",
                keyValue: "01912e03-51a6-7a70-bf92-4ae0ef632499",
                column: "ConcurrencyToken",
                value: "04fa8d08-aea9-46b1-ad79-f6e0c9fb735f");

            migrationBuilder.UpdateData(
                schema: "openid",
                table: "OpenIddictScopes",
                keyColumn: "Id",
                keyValue: "01912e03-51a6-7892-9831-0f132b5417a2",
                column: "ConcurrencyToken",
                value: "2fad9c0d-91a6-4fcf-a0c0-5d974ded6ad8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "openid",
                table: "OpenIddictApplications",
                keyColumn: "Id",
                keyValue: "01912e03-51a6-75cd-a1de-3aa4fb87a3d8",
                columns: new[] { "ApplicationType", "ConcurrencyToken" },
                values: new object[] { "web", "34d13a1b-ada5-4bca-848b-b6b64c87106a" });

            migrationBuilder.UpdateData(
                schema: "openid",
                table: "OpenIddictApplications",
                keyColumn: "Id",
                keyValue: "01912e03-51a6-7a70-bf92-4ae0ef632499",
                column: "ConcurrencyToken",
                value: "0954c756-76fe-410c-bd5b-b79fa223c2c4");

            migrationBuilder.UpdateData(
                schema: "openid",
                table: "OpenIddictScopes",
                keyColumn: "Id",
                keyValue: "01912e03-51a6-7892-9831-0f132b5417a2",
                column: "ConcurrencyToken",
                value: "f32921aa-5ed0-41d5-8c92-8a26e4683b94");
        }
    }
}
