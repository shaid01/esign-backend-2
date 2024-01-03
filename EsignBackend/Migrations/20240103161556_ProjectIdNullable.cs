using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsignBackend.Migrations
{
    /// <inheritdoc />
    public partial class ProjectIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_projects_projectid",
                table: "certificates");

            migrationBuilder.AlterColumn<int>(
                name: "projectid",
                table: "certificates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_projects_projectid",
                table: "certificates",
                column: "projectid",
                principalTable: "projects",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_projects_projectid",
                table: "certificates");

            migrationBuilder.AlterColumn<int>(
                name: "projectid",
                table: "certificates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_projects_projectid",
                table: "certificates",
                column: "projectid",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
