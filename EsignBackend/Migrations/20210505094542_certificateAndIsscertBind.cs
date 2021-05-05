using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class certificateAndIsscertBind : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "certificateissuer",
                table: "certificates",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_certificates_certificateissuer",
                table: "certificates",
                column: "certificateissuer");

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_isscert_certificateissuer",
                table: "certificates",
                column: "certificateissuer",
                principalTable: "isscert",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_isscert_certificateissuer",
                table: "certificates");

            migrationBuilder.DropIndex(
                name: "IX_certificates_certificateissuer",
                table: "certificates");

            migrationBuilder.AlterColumn<double>(
                name: "certificateissuer",
                table: "certificates",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
