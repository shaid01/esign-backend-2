using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsignBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes_Customers_Certificates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IdNumber_OrderBy",
                table: "customers",
                column: "idnumber");

            migrationBuilder.CreateIndex(
                name: "IssueDate_OrderBy_Desc",
                table: "certificates",
                column: "issuedate",
                descending: new bool[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IdNumber_OrderBy",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "IssueDate_OrderBy_Desc",
                table: "certificates");
        }
    }
}
