using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppApiService.Infrastructure.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableDataMap02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataMap_DataMap_DataMapChildId",
                table: "DataMap");

            migrationBuilder.RenameColumn(
                name: "DataMapChildId",
                table: "DataMap",
                newName: "DataMapId");

            migrationBuilder.RenameIndex(
                name: "IX_DataMap_DataMapChildId",
                table: "DataMap",
                newName: "IX_DataMap_DataMapId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataMap_DataMap_DataMapId",
                table: "DataMap",
                column: "DataMapId",
                principalTable: "DataMap",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataMap_DataMap_DataMapId",
                table: "DataMap");

            migrationBuilder.RenameColumn(
                name: "DataMapId",
                table: "DataMap",
                newName: "DataMapChildId");

            migrationBuilder.RenameIndex(
                name: "IX_DataMap_DataMapId",
                table: "DataMap",
                newName: "IX_DataMap_DataMapChildId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataMap_DataMap_DataMapChildId",
                table: "DataMap",
                column: "DataMapChildId",
                principalTable: "DataMap",
                principalColumn: "Id");
        }
    }
}
