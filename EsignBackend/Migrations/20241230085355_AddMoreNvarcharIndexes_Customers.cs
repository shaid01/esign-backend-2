using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsignBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreNvarcharIndexes_Customers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "LastFirstName_OrderBy",
                table: "customers",
                columns: new[] { "lastname", "firstname" });

            migrationBuilder.CreateIndex(
                name: "FirstLastName_OrderBy",
                table: "customers",
                columns: new[] { "firstname", "lastname" });

            migrationBuilder.CreateIndex(
                name: "Email_OrderBy",
                table: "customers",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "Company_OrderBy",
                table: "customers",
                column: "company");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Company_OrderBy",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "Email_OrderBy",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "FirstLastName_OrderBy",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "LastFirstName_OrderBy",
                table: "customers");
        }
    }
}
