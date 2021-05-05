using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class certificateAndCerstatusBind : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "certificatestatus",
                table: "certificates",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_certificates_certificatestatus",
                table: "certificates",
                column: "certificatestatus");

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_certificatesstatus_certificatestatus",
                table: "certificates",
                column: "certificatestatus",
                principalTable: "certificatesstatus",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_certificatesstatus_certificatestatus",
                table: "certificates");

            migrationBuilder.DropIndex(
                name: "IX_certificates_certificatestatus",
                table: "certificates");

            migrationBuilder.AlterColumn<double>(
                name: "certificatestatus",
                table: "certificates",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
