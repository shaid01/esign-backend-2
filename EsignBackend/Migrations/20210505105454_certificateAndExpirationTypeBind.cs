using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class certificateAndExpirationTypeBind : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "expire",
                table: "certificates",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_certificates_expire",
                table: "certificates",
                column: "expire");

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_expirationtype_expire",
                table: "certificates",
                column: "expire",
                principalTable: "expirationtype",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_expirationtype_expire",
                table: "certificates");

            migrationBuilder.DropIndex(
                name: "IX_certificates_expire",
                table: "certificates");

            migrationBuilder.AlterColumn<double>(
                name: "expire",
                table: "certificates",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
