using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EsignBackend.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        //public virtual DbSet<Person> Persons { get; set; }

        public virtual DbSet<Attachment> Attachments { get; set; }
        // public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Budesign> Budesigns { get; set; }
        public virtual DbSet<Bulanguage> Bulanguages { get; set; }
        public virtual DbSet<Bumodule> Bumodules { get; set; }
        public virtual DbSet<Bumodulecoderep> Bumodulecodereps { get; set; }
        public virtual DbSet<Bumodulefield> Bumodulefields { get; set; }
        public virtual DbSet<Busetting> Busettings { get; set; }
        public virtual DbSet<Buuser> Buusers { get; set; }
        public virtual DbSet<Callpriority> Callpriorities { get; set; }
        public virtual DbSet<Callstatus> Callstatuses { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<Certificatermeark> Certificatermearks { get; set; }
        public virtual DbSet<Certificateshistory> Certificateshistories { get; set; }
        public virtual DbSet<Certificatesstatus> Certificatesstatuses { get; set; }
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<Custident> Custidents { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Departmant> Departmants { get; set; }
        public virtual DbSet<Docstype> Docstypes { get; set; }
        public virtual DbSet<Expirationtype> Expirationtypes { get; set; }
        public virtual DbSet<Isscert> Isscerts { get; set; }
        public virtual DbSet<Issplace> Issplaces { get; set; }
        public virtual DbSet<Progressreport> Progressreports { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Securityquestion> Securityquestions { get; set; }
        public virtual DbSet<Smartobject> Smartobjects { get; set; }
        public virtual DbSet<Subproject> Subprojects { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Userview> Userviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=CMD-SAARM-LPT\\SQLEXPRESS;Initial Catalog=dbcomsign;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1255_CI_AS");

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.HasNoKey();


                entity.ToTable("attachments");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.File1)
                    .HasMaxLength(100)
                    .HasColumnName("file1")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ticketid).HasColumnName("ticketid");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });


            modelBuilder.Entity<Budesign>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("budesign");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Fieldname)
                    .HasMaxLength(50)
                    .HasColumnName("fieldname")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.File1)
                    .HasMaxLength(100)
                    .HasColumnName("file1")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Lable)
                    .HasMaxLength(50)
                    .HasColumnName("lable")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Value1)
                    .HasMaxLength(50)
                    .HasColumnName("value1")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Bulanguage>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("bulanguages");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Entityid).HasColumnName("entityid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Parentid).HasColumnName("parentid");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Bumodule>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("bumodules");

                entity.Property(e => e.Addcsv)
                    .HasMaxLength(2)
                    .HasColumnName("addcsv")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Addduplicatebutton)
                    .HasMaxLength(2)
                    .HasColumnName("addduplicatebutton")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Addloadcsv)
                    .HasMaxLength(2)
                    .HasColumnName("addloadcsv")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Addmultiactions)
                    .HasMaxLength(2)
                    .HasColumnName("addmultiactions")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Addnav)
                    .HasMaxLength(2)
                    .HasColumnName("addnav")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Addpages)
                    .HasMaxLength(2)
                    .HasColumnName("addpages")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Addprint)
                    .HasMaxLength(2)
                    .HasColumnName("addprint")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Addsearch)
                    .HasMaxLength(2)
                    .HasColumnName("addsearch")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Addsearchbar)
                    .HasMaxLength(2)
                    .HasColumnName("addsearchbar")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Addtrans)
                    .HasMaxLength(2)
                    .HasColumnName("addtrans")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Astree)
                    .HasMaxLength(2)
                    .HasColumnName("astree")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Created)
                    .HasMaxLength(2)
                    .HasColumnName("created")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Createtable)
                    .HasMaxLength(2)
                    .HasColumnName("createtable")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Defaultorder)
                    .HasMaxLength(50)
                    .HasColumnName("defaultorder")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Display)
                    .HasMaxLength(100)
                    .HasColumnName("display")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Icon)
                    .HasMaxLength(30)
                    .HasColumnName("icon")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Monthviewfield)
                    .HasMaxLength(100)
                    .HasColumnName("monthviewfield")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Multilang)
                    .HasMaxLength(2)
                    .HasColumnName("multilang")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Navgrouptitle)
                    .HasMaxLength(30)
                    .HasColumnName("navgrouptitle")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Permissions)
                    .HasColumnName("permissions")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Priority).HasColumnName("priority");

                entity.Property(e => e.Showid)
                    .HasMaxLength(2)
                    .HasColumnName("showid")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Updateddate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateddate");

                entity.Property(e => e.Updateduserid).HasColumnName("updateduserid");
            });

            modelBuilder.Entity<Bumodulecoderep>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("bumodulecoderep");

                entity.Property(e => e.Applytoall)
                    .HasMaxLength(2)
                    .HasColumnName("applytoall")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Parentid).HasColumnName("parentid");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Text1)
                    .HasColumnName("text1")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Text2)
                    .HasColumnName("text2")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Tid).HasColumnName("tid");

                entity.Property(e => e.Times).HasColumnName("times");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Bumodulefield>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("bumodulefields");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Tcontrol)
                    .HasMaxLength(30)
                    .HasColumnName("tcontrol")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Tdata)
                    .HasColumnName("tdata")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Tdefault)
                    .HasColumnName("tdefault")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Tdir)
                    .HasMaxLength(20)
                    .HasColumnName("tdir")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Tdisplay)
                    .HasMaxLength(100)
                    .HasColumnName("tdisplay")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Tfilter)
                    .HasMaxLength(2)
                    .HasColumnName("tfilter")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Thelp)
                    .HasColumnName("thelp")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Tid).HasColumnName("tid");

                entity.Property(e => e.Tlength).HasColumnName("tlength");

                entity.Property(e => e.Tlist)
                    .HasMaxLength(2)
                    .HasColumnName("tlist")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Tlistupdate)
                    .HasMaxLength(2)
                    .HasColumnName("tlistupdate")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Tmax).HasColumnName("tmax");

                entity.Property(e => e.Tmin).HasColumnName("tmin");

                entity.Property(e => e.Tmultiupdate)
                    .HasMaxLength(2)
                    .HasColumnName("tmultiupdate")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Tmust)
                    .HasMaxLength(2)
                    .HasColumnName("tmust")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Tname)
                    .HasMaxLength(100)
                    .HasColumnName("tname")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Tnotr)
                    .HasMaxLength(2)
                    .HasColumnName("tnotr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Tpriority).HasColumnName("tpriority");

                entity.Property(e => e.Ttype)
                    .HasMaxLength(20)
                    .HasColumnName("ttype")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Busetting>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("busettings");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Explain1)
                    .HasColumnName("explain1")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Parentid).HasColumnName("parentid");

                entity.Property(e => e.Priority).HasColumnName("priority");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Buuser>(entity =>
            {
                //entity.HasNoKey();                
                /*entity.HasKey(e => e.Id);
                entity.HasKey(e => e.Username);*/

                entity.HasKey(en => new { en.Id });

                entity.ToTable("buusers");

                entity.Property(e => e.Allowedips)
                    .HasColumnName("allowedips")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Departmantid)
                    .HasColumnName("departmantid")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Elang).HasColumnName("elang");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Expires)
                    .HasColumnType("datetime")
                    .HasColumnName("expires");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(30)
                    .HasColumnName("firstname")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(30)
                    .HasColumnName("lastname")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Parentid).HasColumnName("parentid");

                entity.Property(e => e.Pass)
                    .HasMaxLength(30)
                    .HasColumnName("pass")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Permissions)
                    .HasColumnName("permissions")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Picture)
                    .HasMaxLength(100)
                    .HasColumnName("picture")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Provider).HasColumnName("provider");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Sessionvalues)
                    .HasColumnName("sessionvalues")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Updateddate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateddate");

                entity.Property(e => e.Updateduserid).HasColumnName("updateduserid");

                entity.Property(e => e.Usergroup)
                    .HasMaxLength(30)
                    .HasColumnName("usergroup")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Userlevel)
                    .HasMaxLength(1)
                    .HasColumnName("userlevel")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .HasColumnName("username")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Callpriority>(entity =>
            {
                //entity.HasNoKey();
                entity.HasKey(en => new { en.Id });

                entity.ToTable("callpriority");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Callstatus>(entity =>
            {
                // entity.HasNoKey();
                entity.HasKey(en => new { en.Id });

                entity.ToTable("callstatus");

                entity.Property(e => e.Color)
                    .HasMaxLength(50)
                    .HasColumnName("color")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Certificate>(entity =>
            {
                //entity.HasNoKey();
                entity.HasKey(en => new { en.Id });

                entity.ToTable("certificates");

                entity.Property(e => e.Certificateissuer).HasColumnName("certificateissuer");

                entity.Property(e => e.Certificatestatus).HasColumnName("certificatestatus");

                entity.Property(e => e.Company)
                    .HasMaxLength(50)
                    .HasColumnName("company");

                entity.Property(e => e.Customerid).HasColumnName("customerid");

                entity.Property(e => e.Docstype).HasColumnName("docstype");

                entity.Property(e => e.Email)
                    .HasMaxLength(70)
                    .HasColumnName("email");

                entity.Property(e => e.Expire).HasColumnName("expire");

                entity.Property(e => e.Expiredate)
                    .HasColumnType("datetime")
                    .HasColumnName("expiredate");

                entity.Property(e => e.Hotem)
                    .HasMaxLength(50)
                    .HasColumnName("hotem");

                entity.Property(e => e.Hpnumber)
                    .HasMaxLength(50)
                    .HasColumnName("hpnumber");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Identify).HasColumnName("identify");

                entity.Property(e => e.Issuedate)
                    .HasColumnType("datetime")
                    .HasColumnName("issuedate");

                entity.Property(e => e.Issuerplace).HasColumnName("issuerplace");

                entity.Property(e => e.Job)
                    .HasMaxLength(50)
                    .HasColumnName("job");

                entity.Property(e => e.Licenceid)
                    .HasMaxLength(50)
                    .HasColumnName("licenceid");

                entity.Property(e => e.Passportid)
                    .HasMaxLength(50)
                    .HasColumnName("passportid");

                entity.Property(e => e.Project).HasColumnName("project");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Remarksdesc).HasColumnName("remarksdesc");

                entity.Property(e => e.Securityansware)
                    //.HasMaxLength(70)
                    .HasColumnName("securityansware");

                entity.Property(e => e.Securityquestion).HasColumnName("securityquestion");

                entity.Property(e => e.Smartobject).HasColumnName("smartobject");

                entity.Property(e => e.Subproject).HasColumnName("subproject");
            });

            modelBuilder.Entity<Certificatermeark>(entity =>
            {
                //entity.HasNoKey();
                entity.HasKey(en => new { en.Id });

                entity.ToTable("certificatermearks");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Certificateshistory>(entity =>
            {
                //entity.HasNoKey();
                entity.HasKey(en => new { en.Id });

                entity.ToTable("certificateshistory");

                entity.Property(e => e.Certificateid).HasColumnName("certificateid");

                entity.Property(e => e.Certificateissuer)
                    .HasMaxLength(50)
                    .HasColumnName("certificateissuer")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Certificatestatus).HasColumnName("certificatestatus");

                entity.Property(e => e.Company)
                    .HasMaxLength(50)
                    .HasColumnName("company")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Customerid).HasColumnName("customerid");

                entity.Property(e => e.Docstype).HasColumnName("docstype");

                entity.Property(e => e.Email)
                    .HasMaxLength(70)
                    .HasColumnName("email")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Expire).HasColumnName("expire");

                entity.Property(e => e.Expiredate)
                    .HasColumnType("datetime")
                    .HasColumnName("expiredate");

                entity.Property(e => e.Hotem)
                    .HasMaxLength(50)
                    .HasColumnName("hotem")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Hpnumber)
                    .HasMaxLength(50)
                    .HasColumnName("hpnumber")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Identify)
                    .HasMaxLength(50)
                    .HasColumnName("identify")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Issuedate)
                    .HasColumnType("datetime")
                    .HasColumnName("issuedate");

                entity.Property(e => e.Issuerplace)
                    .HasMaxLength(50)
                    .HasColumnName("issuerplace")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Licenceid)
                    .HasMaxLength(50)
                    .HasColumnName("licenceid")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Passportid)
                    .HasMaxLength(50)
                    .HasColumnName("passportid")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Project).HasColumnName("project");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Remarksdesc)
                    .HasColumnName("remarksdesc")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Securityansware)
                    .HasMaxLength(50)
                    .HasColumnName("securityansware")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Securityquestion).HasColumnName("securityquestion");

                entity.Property(e => e.Smartobject).HasColumnName("smartobject");

                entity.Property(e => e.Subproject).HasColumnName("subproject");

                entity.Property(e => e.Updateddate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateddate");

                entity.Property(e => e.Updateduserid).HasColumnName("updateduserid");
            });

            modelBuilder.Entity<Certificatesstatus>(entity =>
            {
                // entity.HasNoKey();
                entity.HasKey(en => new { en.Id });


                entity.ToTable("certificatesstatus");

                entity.Property(e => e.Color)
                    .HasMaxLength(50)
                    .HasColumnName("color")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Character>(entity =>
            {
                entity.ToTable("characters");
            });

            modelBuilder.Entity<Custident>(entity =>
            {
                //entity.HasNoKey();
                entity.HasKey(en => new { en.Id });

                entity.ToTable("custident");

                entity.Property(e => e.Active)
                    .HasMaxLength(2)
                    .HasColumnName("active")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(en => new { en.Id });
                //entity.HasNoKey();

                entity.ToTable("customers");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Calleruserid).HasColumnName("calleruserid");

                entity.Property(e => e.Certificates)
                    .HasMaxLength(20)
                    .HasColumnName("certificates")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Company)
                    .HasMaxLength(50)
                    .HasColumnName("company")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Email)
                    .HasMaxLength(70)
                    .HasColumnName("email")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .HasColumnName("firstname");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Idnumber)
                    .HasMaxLength(50)
                    .HasColumnName("idnumber")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .HasColumnName("lastname")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Mobile1)
                    .HasMaxLength(20)
                    .HasColumnName("mobile1")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Phone1)
                    .HasMaxLength(20)
                    .HasColumnName("phone1")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Securityansware)
                    //.HasMaxLength(50)
                    .HasColumnName("securityansware")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Securityquestion).HasColumnName("securityquestion");

                entity.Property(e => e.Temp)
                    .HasMaxLength(50)
                    .HasColumnName("temp")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Departmant>(entity =>
            {
                // entity.HasNoKey();
                entity.HasKey(en => new { en.Id });

                entity.ToTable("departmants");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Docstype>(entity =>
            {
                // entity.HasNoKey();
                entity.HasKey(en => new { en.Id });

                entity.ToTable("docstype");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Expirationtype>(entity =>
            {
                //entity.HasNoKey();
                entity.HasKey(en => new { en.Id });

                entity.ToTable("expirationtype");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Isscert>(entity =>
            {
                //entity.HasNoKey();
                entity.HasKey(en => new { en.Id });

                entity.ToTable("isscert");

                entity.Property(e => e.Active)
                    .HasMaxLength(2)
                    .HasColumnName("active")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Issplace>(entity =>
            {
                //entity.HasNoKey();

                entity.HasKey(en => new { en.Id });

                entity.ToTable("issplace");

                entity.Property(e => e.Active)
                    .HasMaxLength(2)
                    .HasColumnName("active")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            /* modelBuilder.Entity<Post>(entity =>
             {
                 entity.Property(e => e.Content).HasColumnType("ntext");

                 entity.Property(e => e.Title).HasMaxLength(200);

                 entity.HasOne(d => d.Blog)
                     .WithMany(p => p.Posts)
                     .HasForeignKey(d => d.BlogId)
                     .HasConstraintName("FK_dbo.Posts_dbo.Blogs_BlogId");
             });*/

            modelBuilder.Entity<Progressreport>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("progressreport");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ticketid).HasColumnName("ticketid");

                entity.Property(e => e.Updateddate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateddate");

                entity.Property(e => e.Updateduserid).HasColumnName("updateduserid");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                //entity.HasNoKey();
                entity.HasKey(en => new { en.Id });

                entity.ToTable("projects");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Securityquestion>(entity =>
            {
                //entity.HasNoKey();
                entity.HasKey(en => new { en.Id });

                entity.ToTable("securityquestions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Smartobject>(entity =>
            {
                // entity.HasNoKey();
                entity.HasKey(en => new { en.Id });

                entity.ToTable("smartobjects");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Subproject>(entity =>
            {
                //entity.HasNoKey();
                entity.HasKey(en => new { en.Id });


                entity.ToTable("subproject");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Project).HasColumnName("project");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });


            modelBuilder.Entity<Subproject>().HasOne<Project>(sp => sp.RelatedProject).
                WithMany(p => p.Subprojects).HasForeignKey(sp => sp.Project);

            modelBuilder.Entity<Certificate>().HasOne<Project>(c => c.RelatedProject).
                WithMany(pr => pr.Certificates).HasForeignKey(c => c.Project);

            modelBuilder.Entity<Certificate>().HasOne<Subproject>(c => c.RelatedSubProject).
                WithMany(sp => sp.Certificates).HasForeignKey(c => c.Subproject);

            modelBuilder.Entity<Certificate>().HasOne<Issplace>(c => c.RelatedIssuerPlace).
                WithMany(ip => ip.Certificates).HasForeignKey(c => c.Issuerplace);

            modelBuilder.Entity<Certificate>().HasOne<Isscert>(c => c.RelatedCertificateissuer).
                WithMany(ci => ci.Certificates).HasForeignKey(c => c.Certificateissuer);

            modelBuilder.Entity<Certificate>().HasOne<Custident>(c => c.RelatedCustomerIdentifier).
                WithMany(ci => ci.Certificates).HasForeignKey(c => c.Identify);

            modelBuilder.Entity<Certificate>().HasOne<Customer>(c => c.RelatedCustomer).
                WithMany(customer => customer.CustomerCertificates).HasForeignKey(c => c.Customerid);

            modelBuilder.Entity<Certificate>().HasOne<Certificatesstatus>(c => c.RelatedCertificatesstatus).
                WithMany(cerStatus => cerStatus.Certificates).HasForeignKey(c => c.Certificatestatus);

            modelBuilder.Entity<Certificate>().HasOne<Smartobject>(c => c.RelatedSmartObject).
                WithMany(so => so.Certificates).HasForeignKey(c => c.Smartobject);

            modelBuilder.Entity<Certificate>().HasOne<Expirationtype>(c => c.RelatedExpiration).
                WithMany(et => et.Certificates).HasForeignKey(c => c.Expire);

            modelBuilder.Entity<Certificate>().HasOne<Securityquestion>(c => c.RelatedSecurityquestion).
                WithMany(sq => sq.Certificates).HasForeignKey(c => c.Securityquestion).
                OnDelete(DeleteBehavior.Restrict); ;

            modelBuilder.Entity<Certificate>().HasOne<Docstype>(c => c.RelatedDocsType).
                WithMany(dt => dt.Certificates).HasForeignKey(c => c.Docstype);


            modelBuilder.Entity<Customer>().HasOne<Securityquestion>(cu => cu.RelatedSecurityquestion)
                .WithMany(sq => sq.Customers).HasForeignKey(cu => cu.Securityquestion)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Certificateshistory>().HasOne<Customer>(ch => ch.RelatedCustomer).WithMany(cu => cu.CustomerHistoryCertificates).HasForeignKey(ch => ch.Updateduserid);
            modelBuilder.Entity<Certificateshistory>().HasOne<Project>(ch => ch.RelatedProject).WithMany(p => p.HistoryCertificates).HasForeignKey(ch => ch.Project);
            modelBuilder.Entity<Certificateshistory>().HasOne<Subproject>(ch => ch.RelatedSubProject).WithMany(sp => sp.HistoryCertificates).HasForeignKey(ch => ch.Subproject).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Certificateshistory>().HasOne<Expirationtype>(ch => ch.RelatedExpiration).WithMany(et => et.HistoryCertificates).HasForeignKey(ch => ch.Expire);
            modelBuilder.Entity<Certificateshistory>().HasOne<Certificatesstatus>(ch => ch.RelatedCertificateStatus).WithMany(cs => cs.HistoryCertificates).HasForeignKey(ch => ch.Certificatestatus);
            modelBuilder.Entity<Certificateshistory>().HasOne<Smartobject>(ch => ch.RelatedSmartObject).WithMany(so => so.HistoryCertificates).HasForeignKey(ch => ch.Smartobject);
            modelBuilder.Entity<Certificateshistory>().HasOne<Securityquestion>(ch => ch.RelatedSecurityQuestion).WithMany(sq => sq.HistoryCertificates).HasForeignKey(ch => ch.Securityquestion);
            modelBuilder.Entity<Certificateshistory>().HasOne<Docstype>(ch => ch.RelatedDocsType).WithMany(dt => dt.HistoryCertificates).HasForeignKey(ch => ch.Docstype);
            modelBuilder.Entity<Certificateshistory>().HasOne<Certificate>(ch => ch.RelatedCertificate).WithMany(c => c.HistoryCertificates).HasForeignKey(ch => ch.Certificateid);
            modelBuilder.Entity<Certificateshistory>().HasOne<Buuser>(ch => ch.RelatedUser).WithMany(us => us.HistoryCertificates).HasForeignKey(ch => ch.Updateduserid);

            







            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tickets");

                entity.Property(e => e.Calleruserid).HasColumnName("calleruserid");

                entity.Property(e => e.Closerequest)
                    .HasMaxLength(2)
                    .HasColumnName("closerequest")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Comefrom)
                    .HasMaxLength(50)
                    .HasColumnName("comefrom")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Departmantid).HasColumnName("departmantid");

                entity.Property(e => e.Donedate)
                    .HasColumnType("datetime")
                    .HasColumnName("donedate");

                entity.Property(e => e.Duedate)
                    .HasColumnType("datetime")
                    .HasColumnName("duedate");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Lastdescription)
                    .HasColumnName("lastdescription")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Lastupdater).HasColumnName("lastupdater");

                entity.Property(e => e.Priorityd).HasColumnName("priorityd");

                entity.Property(e => e.Project).HasColumnName("project");

                entity.Property(e => e.Schedule)
                    .HasColumnType("datetime")
                    .HasColumnName("schedule");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Sulution)
                    .HasMaxLength(50)
                    .HasColumnName("sulution")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Ticketname)
                    .HasMaxLength(50)
                    .HasColumnName("ticketname")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Transferto).HasColumnName("transferto");

                entity.Property(e => e.Updateddate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateddate");

                entity.Property(e => e.Updateduserid).HasColumnName("updateduserid");
            });

            modelBuilder.Entity<Userview>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("userviews");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Sessionvalues)
                    .HasColumnName("sessionvalues")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
