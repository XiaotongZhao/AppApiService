using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppApiService.Infrastructure.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTablePipeline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Desciption",
                table: "Pipelines",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Pipelines",
                newName: "Desciption");
        }
    }
}
