using EsignBackend.Models.DTOs.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EsignBackend.Models.DTOs
{
    public class CertificateDetailsDTO
    {

        public CertificateDetailsDTO(CertificateDetails certificate)
        {
            if (certificate == null)
                return;

            Id = certificate.Id;
            CustomerName = HttpUtility.HtmlDecode(certificate.CustomerName);
            CustomerIdentifier = certificate.CustomerIdentifier;
            CustomerIdentifierId = certificate.CustomerIdentifierId;
            Company = HttpUtility.HtmlDecode(certificate.Company);
            Hpnumber = certificate.Hpnumber;
            Job = certificate.Job;
            Issuedate = certificate.Issuedate;
            Expiredate = certificate.Expiredate;
            Project = new ProjectDTO(certificate.Project);
            SubProject = new SubprojectDTO(certificate.SubProject);
            Expire = new ExpirationtypeDTO(certificate.Expire);
            Certificatesstatus = new CertificatesstatusDTO(certificate.Certificatesstatus);
            CertificateIssuer = new IsscertDTO(certificate.CertificateIssuer);
            CertificateLocation = new IssplaceDTO(certificate.CertificateLocation);
            RelatedCustomer = new CustomerDTO(certificate.RelatedCustomer);
            RelatedCustomerIdentifier = new CustidentDTO(certificate.RelatedCustomerIdentifier);
            //06-DEC-2024
            Smartobject = new SmartobjectDTO(certificate.Smartobject);
            Docstype = new DocstypeDTO(certificate.Docstype);
            RelatedSecurityQuestion = new SecurityquestionDTO(certificate.RelatedSecurityQuestion);
            Securityanswer = certificate.Securityanswer;
            Email = certificate.Email;
            Licenseid = certificate.Licenseid;
        }
        public int Id { get; set; }
        public ProjectDTO Project { get; set; }
        public SubprojectDTO SubProject { get; set; }
        public ExpirationtypeDTO Expire { get; set; }
        public SmartobjectDTO Smartobject { get; set; }
        public CertificatesstatusDTO Certificatesstatus { get; set; }
        //  public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        //   public int CustomerIdInDb { get; set; }
        public DocstypeDTO Docstype { get; set; }
        public IsscertDTO CertificateIssuer { get; set; }
        public IssplaceDTO CertificateLocation { get; set; }
        public string CustomerIdentifier { get; set; }
        public int CustomerIdentifierId { get; set; }
        public string Company { get; set; }
        public string Hpnumber { get; set; }
        public string Email { get; set; }
        public string Passportid { get; set; }
        public string Licenseid { get; set; }
        //    public string Signer { get; set; }
        //     public double? Securityquestion { get; set; }
        public SecurityquestionDTO RelatedSecurityQuestion { get; set; }
        public string Securityanswer { get; set; }
        public string Remarks { get; set; }
        //     public string Remarkdesc { get; set; }
        public string Job { get; set; }
        public DateTime? Issuedate { get; set; }
        public DateTime? Expiredate { get; set; }
        public CustomerDTO RelatedCustomer { get; set; }
        public CustidentDTO RelatedCustomerIdentifier { get; set; }
    }
}
