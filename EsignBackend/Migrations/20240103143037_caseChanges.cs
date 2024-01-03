using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsignBackend.Migrations
{
    /// <inheritdoc />
    public partial class caseChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_expirationtype_expire",
                table: "certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_certificateshistory_expirationtype_expire",
                table: "certificateshistory");

            migrationBuilder.RenameColumn(
                name: "expire",
                table: "certificateshistory",
                newName: "expirationtypeid");

            migrationBuilder.RenameIndex(
                name: "IX_certificateshistory_expire",
                table: "certificateshistory",
                newName: "IX_certificateshistory_expirationtypeid");

            migrationBuilder.RenameColumn(
                name: "expire",
                table: "certificates",
                newName: "expirationtypeid");

            migrationBuilder.RenameIndex(
                name: "IX_certificates_expire",
                table: "certificates",
                newName: "IX_certificates_expirationtypeid");

            migrationBuilder.AlterColumn<bool>(
                name: "active",
                table: "custident",
                type: "bit",
                maxLength: 2,
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2,
                oldNullable: true,
                oldCollation: "SQL_Latin1_General_CP1_CI_AS");

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_expirationtype_expirationtypeid",
                table: "certificates",
                column: "expirationtypeid",
                principalTable: "expirationtype",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_certificateshistory_expirationtype_expirationtypeid",
                table: "certificateshistory",
                column: "expirationtypeid",
                principalTable: "expirationtype",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_expirationtype_expirationtypeid",
                table: "certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_certificateshistory_expirationtype_expirationtypeid",
                table: "certificateshistory");

            migrationBuilder.RenameColumn(
                name: "expirationtypeid",
                table: "certificateshistory",
                newName: "expire");

            migrationBuilder.RenameIndex(
                name: "IX_certificateshistory_expirationtypeid",
                table: "certificateshistory",
                newName: "IX_certificateshistory_expire");

            migrationBuilder.RenameColumn(
                name: "expirationtypeid",
                table: "certificates",
                newName: "expire");

            migrationBuilder.RenameIndex(
                name: "IX_certificates_expirationtypeid",
                table: "certificates",
                newName: "IX_certificates_expire");

            migrationBuilder.AlterColumn<string>(
                name: "active",
                table: "custident",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true,
                collation: "SQL_Latin1_General_CP1_CI_AS",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldMaxLength: 2);

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_expirationtype_expire",
                table: "certificates",
                column: "expire",
                principalTable: "expirationtype",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_certificateshistory_expirationtype_expire",
                table: "certificateshistory",
                column: "expire",
                principalTable: "expirationtype",
                principalColumn: "id");
        }
    }
}
