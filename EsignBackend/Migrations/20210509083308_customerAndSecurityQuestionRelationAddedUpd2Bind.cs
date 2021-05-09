using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class customerAndSecurityQuestionRelationAddedUpd2Bind : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropForeignKey(
                name: "FK_certificates_securityquestions_securityquestion",
                table: "certificates");

  

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_securityquestions_securityquestion",
                table: "certificates",
                column: "securityquestion",
                principalTable: "securityquestions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_customers_securityquestions_securityquestion",
                table: "customers",
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

            migrationBuilder.DropForeignKey(
                name: "FK_customers_securityquestions_securityquestion",
                table: "customers");

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_securityquestions_securityquestion",
                table: "certificates",
                column: "securityquestion",
                principalTable: "securityquestions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_customers_securityquestions_securityquestion",
                table: "customers",
                column: "securityquestion",
                principalTable: "securityquestions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
