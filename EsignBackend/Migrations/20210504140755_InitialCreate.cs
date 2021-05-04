using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EsignBackend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "attachments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    file1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    ticketid = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "budesign",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    lable = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    fieldname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    value1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    file1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "bulanguages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    parentid = table.Column<double>(type: "float", nullable: true),
                    entityid = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "bumodulecoderep",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    times = table.Column<double>(type: "float", nullable: true),
                    text1 = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    text2 = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    tid = table.Column<double>(type: "float", nullable: true),
                    parentid = table.Column<double>(type: "float", nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    applytoall = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "bumodulefields",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    tname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    tdisplay = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    ttype = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    tlength = table.Column<double>(type: "float", nullable: true),
                    tdefault = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    tdir = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    tcontrol = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    tpriority = table.Column<double>(type: "float", nullable: true),
                    tid = table.Column<double>(type: "float", nullable: true),
                    tfilter = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    tmultiupdate = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    tdata = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    tmust = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    thelp = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    tmin = table.Column<double>(type: "float", nullable: true),
                    tmax = table.Column<double>(type: "float", nullable: true),
                    tlistupdate = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    tnotr = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    tlist = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "bumodules",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    display = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    astree = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    addsearch = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    addnav = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    createtable = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    created = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    multilang = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    icon = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    addcsv = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    addsearchbar = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    addmultiactions = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    addduplicatebutton = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    addpages = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    priority = table.Column<int>(type: "int", nullable: true),
                    addtrans = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    showid = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    addloadcsv = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    addprint = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    monthviewfield = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    navgrouptitle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    updateduserid = table.Column<double>(type: "float", nullable: true),
                    updateddate = table.Column<DateTime>(type: "datetime", nullable: true),
                    defaultorder = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    permissions = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "busettings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    explain1 = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    priority = table.Column<double>(type: "float", nullable: true),
                    parentid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "buusers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    userlevel = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    usergroup = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    firstname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    lastname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    pass = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    picture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    updateduserid = table.Column<double>(type: "float", nullable: true),
                    updateddate = table.Column<DateTime>(type: "datetime", nullable: true),
                    parentid = table.Column<double>(type: "float", nullable: true),
                    permissions = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    allowedips = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    expires = table.Column<DateTime>(type: "datetime", nullable: true),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    sessionvalues = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    provider = table.Column<double>(type: "float", nullable: true),
                    elang = table.Column<double>(type: "float", nullable: true),
                    departmantid = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buusers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "callpriority",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_callpriority", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "callstatus",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_callstatus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "certificatermearks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_certificatermearks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "certificates",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    project = table.Column<double>(type: "float", nullable: true),
                    company = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    hpnumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    issuedate = table.Column<DateTime>(type: "datetime", nullable: true),
                    expiredate = table.Column<DateTime>(type: "datetime", nullable: true),
                    expire = table.Column<double>(type: "float", nullable: true),
                    smartobject = table.Column<double>(type: "float", nullable: true),
                    certificatestatus = table.Column<double>(type: "float", nullable: true),
                    customerid = table.Column<double>(type: "float", nullable: true),
                    subproject = table.Column<double>(type: "float", nullable: true),
                    docstype = table.Column<double>(type: "float", nullable: true),
                    passportid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    licenceid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    hotem = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    securityquestion = table.Column<double>(type: "float", nullable: true),
                    securityansware = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    remarksdesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    job = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    identify = table.Column<double>(type: "float", nullable: true),
                    certificateissuer = table.Column<double>(type: "float", nullable: true),
                    issuerplace = table.Column<double>(type: "float", nullable: true),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_certificates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "certificateshistory",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerid = table.Column<double>(type: "float", nullable: true),
                    email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    project = table.Column<double>(type: "float", nullable: true),
                    subproject = table.Column<double>(type: "float", nullable: true),
                    company = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    hpnumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    hotem = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    issuedate = table.Column<DateTime>(type: "datetime", nullable: true),
                    expire = table.Column<double>(type: "float", nullable: true),
                    expiredate = table.Column<DateTime>(type: "datetime", nullable: true),
                    certificatestatus = table.Column<double>(type: "float", nullable: true),
                    smartobject = table.Column<double>(type: "float", nullable: true),
                    remarksdesc = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    securityquestion = table.Column<double>(type: "float", nullable: true),
                    securityansware = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    docstype = table.Column<double>(type: "float", nullable: true),
                    passportid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    licenceid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    identify = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    certificateissuer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    issuerplace = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    certificateid = table.Column<double>(type: "float", nullable: true),
                    updateduserid = table.Column<double>(type: "float", nullable: true),
                    updateddate = table.Column<DateTime>(type: "datetime", nullable: true),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_certificateshistory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "certificatesstatus",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_certificatesstatus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "custident",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    active = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_custident", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idnumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    firstname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    lastname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    phone1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    mobile1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    securityquestion = table.Column<double>(type: "float", nullable: true),
                    securityansware = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    certificates = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    temp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    calleruserid = table.Column<double>(type: "float", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    company = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "departmants",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departmants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "docstype",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_docstype", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "expirationtype",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expirationtype", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "isscert",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    active = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_isscert", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "issplace",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    active = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_issplace", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "progressreport",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    ticketid = table.Column<double>(type: "float", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    updateduserid = table.Column<double>(type: "float", nullable: true),
                    updateddate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "securityquestions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_securityquestions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "smartobjects",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_smartobjects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "subproject",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    project = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subproject", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    departmantid = table.Column<double>(type: "float", nullable: true),
                    status = table.Column<double>(type: "float", nullable: true),
                    priorityd = table.Column<double>(type: "float", nullable: true),
                    updateduserid = table.Column<double>(type: "float", nullable: true),
                    updateddate = table.Column<DateTime>(type: "datetime", nullable: true),
                    transferto = table.Column<double>(type: "float", nullable: true),
                    schedule = table.Column<DateTime>(type: "datetime", nullable: true),
                    lastupdater = table.Column<double>(type: "float", nullable: true),
                    lastdescription = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    calleruserid = table.Column<double>(type: "float", nullable: true),
                    donedate = table.Column<DateTime>(type: "datetime", nullable: true),
                    closerequest = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    duedate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ticketname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    comefrom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    sulution = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    project = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "userviews",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    sessionvalues = table.Column<string>(type: "nvarchar(max)", nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    userid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ProjectId",
                table: "Persons",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attachments");

            migrationBuilder.DropTable(
                name: "budesign");

            migrationBuilder.DropTable(
                name: "bulanguages");

            migrationBuilder.DropTable(
                name: "bumodulecoderep");

            migrationBuilder.DropTable(
                name: "bumodulefields");

            migrationBuilder.DropTable(
                name: "bumodules");

            migrationBuilder.DropTable(
                name: "busettings");

            migrationBuilder.DropTable(
                name: "buusers");

            migrationBuilder.DropTable(
                name: "callpriority");

            migrationBuilder.DropTable(
                name: "callstatus");

            migrationBuilder.DropTable(
                name: "certificatermearks");

            migrationBuilder.DropTable(
                name: "certificates");

            migrationBuilder.DropTable(
                name: "certificateshistory");

            migrationBuilder.DropTable(
                name: "certificatesstatus");

            migrationBuilder.DropTable(
                name: "characters");

            migrationBuilder.DropTable(
                name: "custident");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "departmants");

            migrationBuilder.DropTable(
                name: "docstype");

            migrationBuilder.DropTable(
                name: "expirationtype");

            migrationBuilder.DropTable(
                name: "isscert");

            migrationBuilder.DropTable(
                name: "issplace");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "progressreport");

            migrationBuilder.DropTable(
                name: "securityquestions");

            migrationBuilder.DropTable(
                name: "smartobjects");

            migrationBuilder.DropTable(
                name: "subproject");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "userviews");

            migrationBuilder.DropTable(
                name: "projects");
        }
    }
}
