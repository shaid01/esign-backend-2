using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class historyCertificateRelationsBindingUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
     /*       migrationBuilder.CreateIndex(
                name: "IX_certificateshistory_subproject",
                table: "certificateshistory",
                column: "subproject");*/

   /*         migrationBuilder.AddForeignKey(
                name: "FK_certificateshistory_subproject_subproject",
                table: "certificateshistory",
                column: "subproject",
                principalTable: "subproject",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificateshistory_subproject_subproject",
                table: "certificateshistory");

            migrationBuilder.DropIndex(
                name: "IX_certificateshistory_subproject",
                table: "certificateshistory");
        }
    }
}
