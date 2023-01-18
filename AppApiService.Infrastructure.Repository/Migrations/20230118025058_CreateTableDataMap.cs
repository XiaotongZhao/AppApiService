using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppApiService.Infrastructure.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableDataMap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Tests",
                type: "nvarchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Tests",
                type: "nvarchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)");

            migrationBuilder.CreateTable(
                name: "DataMap",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MapName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MapType = table.Column<int>(type: "int", nullable: false),
                    DataMapChildId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataMap_DataMap_DataMapChildId",
                        column: x => x.DataMapChildId,
                        principalTable: "DataMap",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataMap_DataMapChildId",
                table: "DataMap",
                column: "DataMapChildId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataMap");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Tests",
                type: "nvarchar(500)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Tests",
                type: "nvarchar(500)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldNullable: true);
        }
    }
}
