using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class pksPt2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
            name: "PK_Isscert",
            table: "isscert",
            column: "id");

            migrationBuilder.AddPrimaryKey(
            name: "PK_Expirationtype",
            table: "expirationtype",
            column: "id");

            migrationBuilder.AddPrimaryKey(
            name: "PK_Docstype",
            table: "docstype",
            column: "id");

            migrationBuilder.AddPrimaryKey(
            name: "PK_Custident",
            table: "custident",
            column: "id");




        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
