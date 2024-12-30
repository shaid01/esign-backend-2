using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

#nullable disable

namespace EsignBackend.Migrations
{
    /// <inheritdoc />
    public partial class ReduceSizeOfNvarchars_Customers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(File.ReadAllText(@"..\EsignBackend\Queries\customers-reduce-size-of-nvarchars.sql"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
