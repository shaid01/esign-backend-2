using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models
{
    public class HistoryCertificateDetails
    {
        public HistoryCertificateDetails() { }
        public HistoryCertificateDetails(Certificateshistory certificateshistory)
        {
            this.Id = certificateshistory.Id;
            this.Project = certificateshistory.RelatedProject;
            this.SubProject = certificateshistory.RelatedSubProject;
            this.Expire = certificateshistory.RelatedExpiration;
            this.Smartobject = certificateshistory.RelatedSmartObject;
            this.Certificatesstatus = certificateshistory.RelatedCertificateStatus;
            //this.CustomerId = certificateshistory.Customerid;
            this.CustomerName = certificateshistory.RelatedCustomer?.Firstname + " " + certificateshistory.RelatedCustomer?.Lastname;
            this.Docstype = certificateshistory.RelatedDocsType;
            this.CertificateIssuer = certificateshistory.Certificateissuer;
            this.CertificateLocation = certificateshistory.Issuerplace;
            this.CustomerIdentifier = certificateshistory.Identify;
            this.Company = certificateshistory.Company;
            this.Hpnumber = certificateshistory.Hpnumber;
            this.Email = certificateshistory.Email;
            this.Passportid = certificateshistory.Passportid;
            this.Licenseid = certificateshistory.Licenceid;
            this.Signer = certificateshistory.Hotem;
            this.RelatedSecurityquestion = certificateshistory.RelatedSecurityQuestion;
            this.Securityquestion = certificateshistory.Securityquestion;
            this.Securityanswer = certificateshistory.Securityansware;
            this.Remarks = certificateshistory.Remarks;
            this.Remarkdesc = certificateshistory.Remarksdesc;
            this.Issuedate = (DateTime)certificateshistory.Issuedate;
            this.Expiredate = (DateTime)certificateshistory.Expiredate;
            this.UpdatedDate = (DateTime)certificateshistory.Updateddate;
            this.UpdatedUserName = this.UpdatedUserName;
            this.Customer = certificateshistory.RelatedCustomer;
            this.AttorneyLicenseNumber = certificateshistory.AttorneyLicenseNumber;

        }

        public int Id { get; set; }
        public Project Project { get; set; }
        public Subproject SubProject { get; set; }
        public Expirationtype Expire { get; set; }
        public Smartobject Smartobject { get; set; }
        public Certificatesstatus Certificatesstatus { get; set; }
        public Securityquestion RelatedSecurityquestion { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Docstype Docstype { get; set; }
        public string CertificateIssuer { get; set; }
        public string CertificateLocation { get; set; }
        public string CustomerIdentifier { get; set; }
        public string Company { get; set; }
        public string Hpnumber { get; set; }
        public string Email { get; set; }
        public string Passportid { get; set; }
        public string Licenseid { get; set; }
        public string Signer { get; set; }
        public int? Securityquestion { get; set; }
        public string Securityanswer { get; set; }
        public string Remarks { get; set; }
        public string Remarkdesc { get; set; }
        public string AttorneyLicenseNumber { get; set; }
        public DateTime Issuedate { get; set; }
        public DateTime Expiredate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedUserName { get; set; }
        public Customer Customer { get; set; }


    }
}