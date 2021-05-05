using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class callpriorityUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         migrationBuilder.AddPrimaryKey(
         name: "PK_Callpriority",
         table: "callpriority",
         column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         migrationBuilder.AddPrimaryKey(
         name: "PK_Callpriority",
         table: "callpriority",
         column: "id");
        }
    }
}
