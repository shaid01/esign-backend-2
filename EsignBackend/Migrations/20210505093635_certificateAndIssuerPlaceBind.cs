using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class certificateAndIssuerPlaceBind : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "issuerplace",
                table: "certificates",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_certificates_issuerplace",
                table: "certificates",
                column: "issuerplace");

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_issplace_issuerplace",
                table: "certificates",
                column: "issuerplace",
                principalTable: "issplace",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_issplace_issuerplace",
                table: "certificates");

            migrationBuilder.DropIndex(
                name: "IX_certificates_issuerplace",
                table: "certificates");

            migrationBuilder.AlterColumn<double>(
                name: "issuerplace",
                table: "certificates",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
