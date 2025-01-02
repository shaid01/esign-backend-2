using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsignBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddIssueExpiryDateFieldsIndexes_Certificates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IssueExpiryDates_Where",
                table: "certificates",
                columns: new[] { "issuedate", "expiredate" });

            migrationBuilder.CreateIndex(
                name: "ExpiryIssueDates_Where_OrderBy",
                table: "certificates",
                columns: new[] { "expiredate", "issuedate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ExpiryIssueDates_Where_OrderBy",
                table: "certificates");

            migrationBuilder.DropIndex(
                name: "IssueExpiryDates_Where",
                table: "certificates");
        }
    }
}
