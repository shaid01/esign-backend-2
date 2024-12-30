using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

#nullable disable

namespace EsignBackend.Migrations
{
    /// <inheritdoc />
    public partial class ReduceSizeOfNvarchars_Certificates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(File.ReadAllText(@"..\EsignBackend\Queries\certificates-reduce-size-of-nvarchars.sql"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
