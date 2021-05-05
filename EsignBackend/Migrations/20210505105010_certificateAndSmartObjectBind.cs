using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class certificateAndSmartObjectBind : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "smartobject",
                table: "certificates",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_certificates_smartobject",
                table: "certificates",
                column: "smartobject");

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_smartobjects_smartobject",
                table: "certificates",
                column: "smartobject",
                principalTable: "smartobjects",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_smartobjects_smartobject",
                table: "certificates");

            migrationBuilder.DropIndex(
                name: "IX_certificates_smartobject",
                table: "certificates");

            migrationBuilder.AlterColumn<double>(
                name: "smartobject",
                table: "certificates",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
