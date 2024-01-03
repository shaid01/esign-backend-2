using EsignBackend.Extensions.EncryptDecrypt;
using EsignBackend.Services.MainServices.Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EsignBackend.Models
{
    public class CertificateDetails
    {
        public CertificateDetails() { }
        public CertificateDetails(Certificate certificate)
        {
            if(certificate == null)
            {
                return;
            }
            this.Id = certificate.Id;
            this.Company = HttpUtility.HtmlDecode(certificate.Company);
            this.Hpnumber = certificate.Hpnumber;
            this.Email = certificate.Email;
            this.Passportid = certificate.PassportId;
            this.Licenseid = certificate.LicenceId;
            this.Signer = HttpUtility.HtmlDecode(certificate.Hotem);
            this.Securityquestion = certificate.SecurityquestionId;
            this.Securityanswer = EncryptDecryptHandler.decryptSecurityAns(certificate.Securityansware);
            this.Remarks = HttpUtility.HtmlDecode(certificate.Remarks);
            this.Remarkdesc = HttpUtility.HtmlDecode(certificate.Remarksdesc);
            this.Job = HttpUtility.HtmlDecode(certificate.Job);
            this.Issuedate = certificate.Issuedate;
            this.Expiredate = certificate.Expiredate;
            this.Project = certificate.RelatedProject;
            this.SubProject = certificate.RelatedSubProject;
            this.Expirationtype = certificate.RelatedExpiration;
            this.Smartobject = certificate.RelatedSmartObject;
            this.Certificatesstatus = certificate.RelatedCertificatesstatus;
            this.RelatedCustomer = certificate.RelatedCustomer;
            this.CustomerName = $"{HttpUtility.HtmlDecode(certificate.RelatedCustomer?.Firstname)} {HttpUtility.HtmlDecode(certificate.RelatedCustomer?.Lastname)}";
            this.Docstype = certificate.RelatedDocsType;
            this.CertificateIssuer = certificate.RelatedCertificateissuer;
            this.CertificateLocation = certificate.RelatedIssuerPlace;
            this.RelatedCustomerIdentifier = certificate.RelatedCustomerIdentifier;
            this.RelatedSecurityQuestion = certificate.RelatedSecurityquestion;
        }

        public int Id { get; set; }
        public Project Project { get; set; }
        public SubProject SubProject { get; set; }
        public ExpirationType Expirationtype { get; set; }
        public SmartObject Smartobject { get; set; }
        public CertificatesStatus Certificatesstatus { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int CustomerIdInDb { get; set; }
        public DocsType Docstype { get; set; }
        public Isscert CertificateIssuer { get; set; }
        public IssPlace CertificateLocation { get; set; }
        public string CustomerIdentifier { get; set; }
        public int CustomerIdentifierId { get; set; }
        public Custident RelatedCustomerIdentifier { get; set; }
        public Customer RelatedCustomer { get; set; }
        public string Company { get; set; }
        public string Hpnumber { get; set; }
        public string Email { get; set; }
        public string Passportid { get; set; }
        public string Licenseid { get; set; }
        public string Signer { get; set; }
        public double? Securityquestion { get; set; }
        public SecurityGuestion RelatedSecurityQuestion { get; set; }
        public string Securityanswer { get; set; }
        public string Remarks { get; set; }
        public string Remarkdesc { get; set; }
        public string Job { get; set; }
        public DateTime? Issuedate { get; set; }
        public DateTime? Expiredate { get; set; }
    }
}