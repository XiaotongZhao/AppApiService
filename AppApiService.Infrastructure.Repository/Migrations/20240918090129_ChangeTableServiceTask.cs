using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppApiService.Infrastructure.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableServiceTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StepNo",
                table: "DeployServiceTasks");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "DeployServiceTasks");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "DeployServices");

            migrationBuilder.RenameColumn(
                name: "ExcuteResult",
                table: "DeployServiceTasks",
                newName: "OutputResult");

            migrationBuilder.AddColumn<int>(
                name: "ServerFileId",
                table: "ServiceTasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StepNo",
                table: "ServiceTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TaskStatus",
                table: "DeployServiceTasks",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "DeployServiceStatus",
                table: "DeployServices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServerFileId",
                table: "ServiceTasks");

            migrationBuilder.DropColumn(
                name: "StepNo",
                table: "ServiceTasks");

            migrationBuilder.DropColumn(
                name: "DeployServiceStatus",
                table: "DeployServices");

            migrationBuilder.RenameColumn(
                name: "OutputResult",
                table: "DeployServiceTasks",
                newName: "ExcuteResult");

            migrationBuilder.AlterColumn<string>(
                name: "TaskStatus",
                table: "DeployServiceTasks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StepNo",
                table: "DeployServiceTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "DeployServiceTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "DeployServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
