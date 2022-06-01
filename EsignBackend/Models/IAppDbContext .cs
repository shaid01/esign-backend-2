using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace EsignBackend.Models
{
    public interface IAppDbContext
    {
        DbSet<Attachment> Attachments { get; set; }
        DbSet<Budesign> Budesigns { get; set; }
        DbSet<Bulanguage> Bulanguages { get; set; }
        DbSet<Bumodule> Bumodules { get; set; }
        DbSet<Bumodulecoderep> Bumodulecodereps { get; set; }
        DbSet<Bumodulefield> Bumodulefields { get; set; }
        DbSet<Busetting> Busettings { get; set; }
        DbSet<Buuser> Buusers { get; set; }
        DbSet<Callpriority> Callpriorities { get; set; }
        DbSet<Callstatus> Callstatuses { get; set; }
        DbSet<Certificate> Certificates { get; set; }
        DbSet<Certificatermeark> Certificatermearks { get; set; }
        DbSet<Certificateshistory> Certificateshistories { get; set; }
        DbSet<Certificatesstatus> Certificatesstatuses { get; set; }
        DbSet<Custident> Custidents { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Departmant> Departmants { get; set; }
        DbSet<Docstype> Docstypes { get; set; }
        DbSet<Expirationtype> Expirationtypes { get; set; }
        DbSet<Isscert> Isscerts { get; set; }
        DbSet<Issplace> Issplaces { get; set; }
        DbSet<Progressreport> Progressreports { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<Securityquestion> Securityquestions { get; set; }
        DbSet<Smartobject> Smartobjects { get; set; }
        DbSet<Subproject> Subprojects { get; set; }
        DbSet<Ticket> Tickets { get; set; }
        DbSet<Userview> Userviews { get; set; }



        DatabaseFacade Database { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
