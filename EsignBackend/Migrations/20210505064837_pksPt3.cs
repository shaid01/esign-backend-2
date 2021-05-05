using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class pksPt3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddPrimaryKey(
            name: "PK_Customers",
            table: "customers",
            column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
