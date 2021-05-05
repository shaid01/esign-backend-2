using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class pks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
            name: "PK_Securityquestions",
            table: "securityquestions",
            column: "id");

            migrationBuilder.AddPrimaryKey(
            name: "PK_Subproject",
            table: "subproject",
            column: "id");

            migrationBuilder.AddPrimaryKey(
            name: "PK_Smartobjects",
             table: "smartobjects",
             column: "id");

            migrationBuilder.AddPrimaryKey(
            name: "PK_Projects",
            table: "projects",
            column: "id");



        }






        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
