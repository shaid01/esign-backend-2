using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class certificateAndCustomerIdentifierBind : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "identify",
                table: "certificates",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_certificates_identify",
                table: "certificates",
                column: "identify");

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_custident_identify",
                table: "certificates",
                column: "identify",
                principalTable: "custident",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_custident_identify",
                table: "certificates");

            migrationBuilder.DropIndex(
                name: "IX_certificates_identify",
                table: "certificates");

            migrationBuilder.AlterColumn<double>(
                name: "identify",
                table: "certificates",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
