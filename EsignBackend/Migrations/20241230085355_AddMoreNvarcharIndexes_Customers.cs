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
                name: "LastFirstName_Where_OrderBy",
                table: "customers",
                columns: new[] { "lastname", "firstname" });

            migrationBuilder.CreateIndex(
                name: "FirstLastName_Where_OrderBy",
                table: "customers",
                columns: new[] { "firstname", "lastname" });

            migrationBuilder.CreateIndex(
                name: "Email_Where_OrderBy",
                table: "customers",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "CompanyCust_Where_OrderBy",
                table: "customers",
                column: "company");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "CompanyCust_Where_OrderBy",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "Email_Where_OrderBy",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "FirstLastName_Where_OrderBy",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "LastFirstName_Where_OrderBy",
                table: "customers");
        }
    }
}
