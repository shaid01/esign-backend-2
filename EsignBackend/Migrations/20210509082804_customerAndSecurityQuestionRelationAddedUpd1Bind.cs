using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class customerAndSecurityQuestionRelationAddedUpd1Bind : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_securityquestions_securityquestion",
                table: "certificates");

            migrationBuilder.AlterColumn<int>(
                name: "securityquestion",
                table: "certificates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_securityquestions_securityquestion",
                table: "certificates",
                column: "securityquestion",
                principalTable: "securityquestions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_securityquestions_securityquestion",
                table: "certificates");

            migrationBuilder.AlterColumn<int>(
                name: "securityquestion",
                table: "certificates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_securityquestions_securityquestion",
                table: "certificates",
                column: "securityquestion",
                principalTable: "securityquestions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
