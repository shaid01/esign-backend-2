using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsignBackend.Migrations
{
    /// <inheritdoc />
    public partial class changeIssplaceActiveToBool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "active",
                table: "issplace",
                type: "bit",
                maxLength: 2,
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2,
                oldNullable: true,
                oldCollation: "SQL_Latin1_General_CP1_CI_AS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "active",
                table: "issplace",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true,
                collation: "SQL_Latin1_General_CP1_CI_AS",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldMaxLength: 2);
        }
    }
}
