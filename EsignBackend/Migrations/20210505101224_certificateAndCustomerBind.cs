using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class certificateAndCustomerBind : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "customerid",
                table: "certificates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_certificates_customerid",
                table: "certificates",
                column: "customerid");

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_customers_customerid",
                table: "certificates",
                column: "customerid",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_customers_customerid",
                table: "certificates");

            migrationBuilder.DropIndex(
                name: "IX_certificates_customerid",
                table: "certificates");

            migrationBuilder.AlterColumn<double>(
                name: "customerid",
                table: "certificates",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
