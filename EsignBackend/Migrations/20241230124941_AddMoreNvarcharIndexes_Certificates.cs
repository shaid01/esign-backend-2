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
                name: "CustomerID_Query",
                table: "certificates",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "Hpnumber_OrderBy",
                table: "certificates",
                column: "hpnumber");

            migrationBuilder.CreateIndex(
                name: "Company_OrderBy",
                table: "certificates",
                column: "company");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
