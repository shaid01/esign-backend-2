using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class certificateAndSecurityquestionBind : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "securityquestion",
                table: "certificates",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_certificates_securityquestion",
                table: "certificates",
                column: "securityquestion");

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_securityquestions_securityquestion",
                table: "certificates",
                column: "securityquestion",
                principalTable: "securityquestions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_securityquestions_securityquestion",
                table: "certificates");

            migrationBuilder.DropIndex(
                name: "IX_certificates_securityquestion",
                table: "certificates");

            migrationBuilder.AlterColumn<double>(
                name: "securityquestion",
                table: "certificates",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
