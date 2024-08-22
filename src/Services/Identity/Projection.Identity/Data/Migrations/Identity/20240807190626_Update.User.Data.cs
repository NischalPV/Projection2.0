using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projection.Identity.Data.Migrations.Identity
{
    /// <inheritdoc />
    public partial class UpdateUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c0aab6ba-cd71-4010-a9dc-e246997d6183",
                columns: new[] { "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "ADMIN.PROJECTION@HOTMAIL.COM", "AQAAAAIAAYagAAAAEFfJ4+BT1xnzAw0bfmAhdrO78PrcMrF9o0IzygLPXqUx9tsm9k7MjorJWiwS/KHUxA==", "admin.projection@hotmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c0aab6ba-cd71-4010-a9dc-e246997d6183",
                columns: new[] { "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "+919888888888", "AQAAAAIAAYagAAAAEEaoFKnrYG/oMlTS9cUhIp9mURVKKdaVQvnLrUfeHs3ykfOVn3kSBeHFhAGetY2EEA==", "+919888888888" });
        }
    }
}
