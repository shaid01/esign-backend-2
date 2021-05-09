using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class customerAndSecurityQuestionRelationAddedUpdBind : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

          
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
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_customers_securityquestion",
                table: "customers",
                column: "securityquestion");

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
