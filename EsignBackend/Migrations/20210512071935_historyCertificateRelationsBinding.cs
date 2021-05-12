using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class historyCertificateRelationsBinding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "updateduserid",
                table: "certificateshistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "subproject",
                table: "certificateshistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "smartobject",
                table: "certificateshistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "securityquestion",
                table: "certificateshistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "project",
                table: "certificateshistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "expire",
                table: "certificateshistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "docstype",
                table: "certificateshistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "customerid",
                table: "certificateshistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "certificatestatus",
                table: "certificateshistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "certificateid",
                table: "certificateshistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_certificateshistory_certificateid",
                table: "certificateshistory",
                column: "certificateid");

            migrationBuilder.CreateIndex(
                name: "IX_certificateshistory_certificatestatus",
                table: "certificateshistory",
                column: "certificatestatus");

            migrationBuilder.CreateIndex(
                name: "IX_certificateshistory_docstype",
                table: "certificateshistory",
                column: "docstype");

            migrationBuilder.CreateIndex(
                name: "IX_certificateshistory_expire",
                table: "certificateshistory",
                column: "expire");

            migrationBuilder.CreateIndex(
                name: "IX_certificateshistory_project",
                table: "certificateshistory",
                column: "project");

            migrationBuilder.CreateIndex(
                name: "IX_certificateshistory_securityquestion",
                table: "certificateshistory",
                column: "securityquestion");

            migrationBuilder.CreateIndex(
                name: "IX_certificateshistory_smartobject",
                table: "certificateshistory",
                column: "smartobject");

            migrationBuilder.CreateIndex(
                name: "IX_certificateshistory_subproject",
                table: "certificateshistory",
                column: "subproject");

            migrationBuilder.CreateIndex(
                name: "IX_certificateshistory_updateduserid",
                table: "certificateshistory",
                column: "updateduserid");

            migrationBuilder.AddForeignKey(
                name: "FK_certificateshistory_buusers_updateduserid",
                table: "certificateshistory",
                column: "updateduserid",
                principalTable: "buusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_certificateshistory_certificates_certificateid",
                table: "certificateshistory",
                column: "certificateid",
                principalTable: "certificates",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_certificateshistory_certificatesstatus_certificatestatus",
                table: "certificateshistory",
                column: "certificatestatus",
                principalTable: "certificatesstatus",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_certificateshistory_customers_updateduserid",
                table: "certificateshistory",
                column: "updateduserid",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_certificateshistory_docstype_docstype",
                table: "certificateshistory",
                column: "docstype",
                principalTable: "docstype",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_certificateshistory_expirationtype_expire",
                table: "certificateshistory",
                column: "expire",
                principalTable: "expirationtype",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_certificateshistory_projects_project",
                table: "certificateshistory",
                column: "project",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_certificateshistory_securityquestions_securityquestion",
                table: "certificateshistory",
                column: "securityquestion",
                principalTable: "securityquestions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_certificateshistory_smartobjects_smartobject",
                table: "certificateshistory",
                column: "smartobject",
                principalTable: "smartobjects",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_certificateshistory_subproject_subproject",
                table: "certificateshistory",
                column: "subproject",
                principalTable: "subproject",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificateshistory_buusers_updateduserid",
                table: "certificateshistory");

            migrationBuilder.DropForeignKey(
                name: "FK_certificateshistory_certificates_certificateid",
                table: "certificateshistory");

            migrationBuilder.DropForeignKey(
                name: "FK_certificateshistory_certificatesstatus_certificatestatus",
                table: "certificateshistory");

            migrationBuilder.DropForeignKey(
                name: "FK_certificateshistory_customers_updateduserid",
                table: "certificateshistory");

            migrationBuilder.DropForeignKey(
                name: "FK_certificateshistory_docstype_docstype",
                table: "certificateshistory");

            migrationBuilder.DropForeignKey(
                name: "FK_certificateshistory_expirationtype_expire",
                table: "certificateshistory");

            migrationBuilder.DropForeignKey(
                name: "FK_certificateshistory_projects_project",
                table: "certificateshistory");

            migrationBuilder.DropForeignKey(
                name: "FK_certificateshistory_securityquestions_securityquestion",
                table: "certificateshistory");

            migrationBuilder.DropForeignKey(
                name: "FK_certificateshistory_smartobjects_smartobject",
                table: "certificateshistory");

            migrationBuilder.DropForeignKey(
                name: "FK_certificateshistory_subproject_subproject",
                table: "certificateshistory");

            migrationBuilder.DropIndex(
                name: "IX_certificateshistory_certificateid",
                table: "certificateshistory");

            migrationBuilder.DropIndex(
                name: "IX_certificateshistory_certificatestatus",
                table: "certificateshistory");

            migrationBuilder.DropIndex(
                name: "IX_certificateshistory_docstype",
                table: "certificateshistory");

            migrationBuilder.DropIndex(
                name: "IX_certificateshistory_expire",
                table: "certificateshistory");

            migrationBuilder.DropIndex(
                name: "IX_certificateshistory_project",
                table: "certificateshistory");

            migrationBuilder.DropIndex(
                name: "IX_certificateshistory_securityquestion",
                table: "certificateshistory");

            migrationBuilder.DropIndex(
                name: "IX_certificateshistory_smartobject",
                table: "certificateshistory");

            migrationBuilder.DropIndex(
                name: "IX_certificateshistory_subproject",
                table: "certificateshistory");

            migrationBuilder.DropIndex(
                name: "IX_certificateshistory_updateduserid",
                table: "certificateshistory");

            migrationBuilder.AlterColumn<double>(
                name: "updateduserid",
                table: "certificateshistory",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "subproject",
                table: "certificateshistory",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "smartobject",
                table: "certificateshistory",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "securityquestion",
                table: "certificateshistory",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "project",
                table: "certificateshistory",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "expire",
                table: "certificateshistory",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "docstype",
                table: "certificateshistory",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "customerid",
                table: "certificateshistory",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "certificatestatus",
                table: "certificateshistory",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "certificateid",
                table: "certificateshistory",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
