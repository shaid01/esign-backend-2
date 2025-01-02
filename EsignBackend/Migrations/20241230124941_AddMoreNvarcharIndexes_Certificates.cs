using EsignBackend.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsignBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreNvarcharIndexes_Certificates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "CustomerID_Foreign",
                table: "certificates",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "Hpnumber_Where_OrderBy",
                table: "certificates",
                column: "hpnumber");

            migrationBuilder.CreateIndex(
                name: "CompanyCert_Where_OrderBy",
                table: "certificates",
                column: "company");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "CompanyCert_Where_OrderBy",
                table: "certificates");

            migrationBuilder.DropIndex(
                name: "Hpnumber_Where_OrderBy",
                table: "certificates");

            migrationBuilder.DropIndex(
                name: "CustomerID_Foreign",
                table: "certificates");
        }
    }
}
