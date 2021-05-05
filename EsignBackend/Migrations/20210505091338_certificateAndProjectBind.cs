using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class certificateAndProjectBind : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "project",
                table: "certificates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_certificates_project",
                table: "certificates",
                column: "project");

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_projects_project",
                table: "certificates",
                column: "project",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_projects_project",
                table: "certificates");

            migrationBuilder.DropIndex(
                name: "IX_certificates_project",
                table: "certificates");

            migrationBuilder.AlterColumn<double>(
                name: "project",
                table: "certificates",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
