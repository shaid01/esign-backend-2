using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace EsignBackend.Models
{
    public interface IAppDbContext
    {
        DbSet<Attachment> Attachments { get; set; }
        DbSet<BuDesign> Budesigns { get; set; }
        DbSet<BuLanguage> Bulanguages { get; set; }
        DbSet<BuModule> Bumodules { get; set; }
        DbSet<BuModuleCoderep> Bumodulecodereps { get; set; }
        DbSet<BuModuleField> Bumodulefields { get; set; }
        DbSet<BuSetting> Busettings { get; set; }
        DbSet<BuUser> Buusers { get; set; }
        DbSet<CallPriority> Callpriorities { get; set; }
        DbSet<Callstatus> Callstatuses { get; set; }
        DbSet<Certificate> Certificates { get; set; }
        DbSet<CertificaterMeark> Certificatermearks { get; set; }
        DbSet<CertificatesHistory> Certificateshistories { get; set; }
        DbSet<CertificatesStatus> Certificatesstatuses { get; set; }
        DbSet<Custident> Custidents { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Department> Departmants { get; set; }
        DbSet<DocsType> Docstypes { get; set; }
        DbSet<ExpirationType> Expirationtypes { get; set; }
        DbSet<Isscert> Isscerts { get; set; }
        DbSet<IssPlace> Issplaces { get; set; }
        DbSet<ProgressReport> Progressreports { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<SecurityGuestion> Securityquestions { get; set; }
        DbSet<SmartObject> Smartobjects { get; set; }
        DbSet<SubProject> Subprojects { get; set; }
        DbSet<Ticket> Tickets { get; set; }
        DbSet<UserView> Userviews { get; set; }



        DatabaseFacade Database { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
