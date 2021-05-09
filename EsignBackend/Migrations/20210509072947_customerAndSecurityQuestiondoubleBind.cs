using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class customerAndSecurityQuestiondoubleBind : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
    



            migrationBuilder.AlterColumn<double>(
                name: "securityquestion",
                table: "customers",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "securityquestion",
                table: "customers",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RelatedSecurityquestionId",
                table: "customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_customers_RelatedSecurityquestionId",
                table: "customers",
                column: "RelatedSecurityquestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_securityquestions_RelatedSecurityquestionId",
                table: "customers",
                column: "RelatedSecurityquestionId",
                principalTable: "securityquestions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
