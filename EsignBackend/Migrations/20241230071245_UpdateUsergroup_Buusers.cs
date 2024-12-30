using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsignBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsergroup_Buusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update buusers set usergroup = 'מנהל' where usergroup = 'אדמין'");
            migrationBuilder.Sql("update buusers set usergroup = 'מנפיק' where usergroup = 'מחדש'");
            migrationBuilder.Sql("update buusers set usergroup = 'תומך' where usergroup = 'אורח'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
