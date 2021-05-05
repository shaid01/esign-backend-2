using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class certificateAndDocstypeBind : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "docstype",
                table: "certificates",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_certificates_docstype",
                table: "certificates",
                column: "docstype");

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_docstype_docstype",
                table: "certificates",
                column: "docstype",
                principalTable: "docstype",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_docstype_docstype",
                table: "certificates");

            migrationBuilder.DropIndex(
                name: "IX_certificates_docstype",
                table: "certificates");

            migrationBuilder.AlterColumn<double>(
                name: "docstype",
                table: "certificates",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
