using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class issplace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
            name: "PK_Issplace",
            table: "issplace",
            column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
