using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class certificateAndSubProjectBind : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "subproject",
                table: "certificates",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_certificates_subproject",
                table: "certificates",
                column: "subproject");

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_subproject_subproject",
                table: "certificates",
                column: "subproject",
                principalTable: "subproject",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_subproject_subproject",
                table: "certificates");

            migrationBuilder.DropIndex(
                name: "IX_certificates_subproject",
                table: "certificates");

            migrationBuilder.AlterColumn<double>(
                name: "subproject",
                table: "certificates",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
