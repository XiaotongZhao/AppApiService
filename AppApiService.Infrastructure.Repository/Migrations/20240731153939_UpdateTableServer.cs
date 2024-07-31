using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppApiService.Infrastructure.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableServer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServerUploadFiles_Servers_ServerId",
                table: "ServerUploadFiles");

            migrationBuilder.DropIndex(
                name: "IX_ServerUploadFiles_ServerId",
                table: "ServerUploadFiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ServerUploadFiles_ServerId",
                table: "ServerUploadFiles",
                column: "ServerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServerUploadFiles_Servers_ServerId",
                table: "ServerUploadFiles",
                column: "ServerId",
                principalTable: "Servers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
