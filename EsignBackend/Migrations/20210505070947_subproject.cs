using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class subproject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "project",
                table: "subproject",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_subproject_project",
                table: "subproject",
                column: "project");

            migrationBuilder.AddForeignKey(
                name: "FK_subproject_projects_project",
                table: "subproject",
                column: "project",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subproject_projects_project",
                table: "subproject");

            migrationBuilder.DropIndex(
                name: "IX_subproject_project",
                table: "subproject");

            migrationBuilder.AlterColumn<double>(
                name: "project",
                table: "subproject",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
