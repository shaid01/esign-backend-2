using EsignBackend.Extensions.EncryptDecrypt;
using EsignBackend.Services.MainServices.Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models
{
    public class CertificateDetails
    {
        public CertificateDetails() { }        


        public CertificateDetails(Certificate certificate)
        {
            this.Id = certificate.Id;
            this.Company = certificate.Company;
            this.Hpnumber = certificate.Hpnumber;
            this.Email = certificate.Email;
            this.Passportid = certificate.Passportid;
            this.Licenseid = certificate.Licenceid;
            this.Signer = certificate.Hotem;
            this.Securityquestion = (double)certificate.Securityquestion;
            this.Securityanswer = EncryptDecryptHandler.decryptSecurityAns(certificate.Securityansware);
            this.Remarks = certificate.Remarks;
            this.Remarkdesc = certificate.Remarksdesc;
            this.Job = certificate.Job;
            this.Issuedate = (DateTime)certificate.Issuedate;
            this.Expiredate = (DateTime)certificate.Expiredate;
            this.Project = certificate.RelatedProject;
            this.SubProject = certificate.RelatedSubProject;
            this.Expire = certificate.RelatedExpiration;
            this.Smartobject = certificate.RelatedSmartObject;
            this.Certificatesstatus = certificate.RelatedCertificatesstatus;
            this.RelatedCustomer = certificate.RelatedCustomer;
            //this.CustomerId = certificate.RelatedCustomer.Idnumber;
            //this.CustomerIdInDb = certificate.RelatedCustomer.Id;
            this.CustomerName = certificate.RelatedCustomer?.Firstname + " " + certificate.RelatedCustomer?.Lastname;
            this.Docstype = certificate.RelatedDocsType;
            this.CertificateIssuer = certificate.RelatedCertificateissuer;
            this.CertificateLocation = certificate.RelatedIssuerPlace;
            this.RelatedCustomerIdentifier = certificate.RelatedCustomerIdentifier;
            //this.CustomerIdentifier = certificate.RelatedCustomerIdentifier.Title;
            // this.CustomerIdentifierId = certificate.RelatedCustomerIdentifier.Id;
            this.RelatedSecurityQuestion = certificate.RelatedSecurityquestion;
        }

        public int Id { get; set; }
        public Project Project { get; set; }
        public Subproject SubProject { get; set; }
        public Expirationtype Expire { get; set; }
        public Smartobject Smartobject { get; set; }
        public Certificatesstatus Certificatesstatus { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int CustomerIdInDb { get; set; }

        public Docstype Docstype { get; set; }
        public Isscert CertificateIssuer { get; set; }
        public Issplace CertificateLocation { get; set; }
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
        public double Securityquestion { get; set; }
        public Securityquestion RelatedSecurityQuestion { get; set; }
        public string Securityanswer { get; set; }
        public string Remarks { get; set; }
        public string Remarkdesc { get; set; }
        public string Job { get; set; }
        public DateTime Issuedate { get; set; }
        public DateTime Expiredate { get; set; }
    }
}
