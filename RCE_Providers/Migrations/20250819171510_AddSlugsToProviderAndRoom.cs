using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RCE_Providers.Migrations
{
    /// <inheritdoc />
    public partial class AddSlugsToProviderAndRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rooms_ProviderId",
                table: "Rooms");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Rooms",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "EscapeRoomProviders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ProviderId_Slug",
                table: "Rooms",
                columns: new[] { "ProviderId", "Slug" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EscapeRoomProviders_Slug",
                table: "EscapeRoomProviders",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rooms_ProviderId_Slug",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_EscapeRoomProviders_Slug",
                table: "EscapeRoomProviders");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "EscapeRoomProviders");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ProviderId",
                table: "Rooms",
                column: "ProviderId");
        }
    }
}
