using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Route02.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FkDepartmentId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FkDepartmentId",
                table: "Employees",
                column: "FkDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_FkDepartmentId",
                table: "Employees",
                column: "FkDepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_FkDepartmentId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_FkDepartmentId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FkDepartmentId",
                table: "Employees");
        }
    }
}
