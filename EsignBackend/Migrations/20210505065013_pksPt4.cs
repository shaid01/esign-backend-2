using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class pksPt4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
            name: "PK_certificates",
            table: "certificates",
            column: "id");

            migrationBuilder.AddPrimaryKey(
            name: "PK_certificatesstatus",
            table: "certificatesstatus",
            column: "id");

            migrationBuilder.AddPrimaryKey(
            name: "PK_departmants",
            table: "departmants",
            column: "id");

            migrationBuilder.AddPrimaryKey(
            name: "PK_certificateshistory",
            table: "certificateshistory",
            column: "id");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
