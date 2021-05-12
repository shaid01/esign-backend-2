using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models.DTOs
{
    public class HistoryCertificateDTO
    {

        public HistoryCertificateDTO(HistoryCertificateDetails historyCertificateDetails)
        {
            this.Id = historyCertificateDetails.Id;
            this.Project = new ProjectDTO(historyCertificateDetails.Project);
            this.SubProject = new SubprojectDTO(historyCertificateDetails.SubProject);
            this.Expire = new ExpirationtypeDTO(historyCertificateDetails.Expire);
            this.Smartobject = new SmartobjectDTO(historyCertificateDetails.Smartobject);
            this.Certificatesstatus = new CertificatesstatusDTO(historyCertificateDetails.Certificatesstatus);
            //this.CustomerId = certificateshistory.Customerid;
            this.CustomerName = historyCertificateDetails.CustomerName;
            this.Docstype = new DocstypeDTO(historyCertificateDetails.Docstype);
            this.CertificateIssuer = historyCertificateDetails.CertificateIssuer;
            this.CertificateLocation = this.CertificateLocation;
            this.CustomerIdentifier = historyCertificateDetails.CustomerIdentifier;
            this.Company = historyCertificateDetails.Company;
            this.Hpnumber = historyCertificateDetails.Hpnumber;
            this.Email = historyCertificateDetails.Email;
            this.Passportid = historyCertificateDetails.Passportid;
            this.Licenseid = historyCertificateDetails.Licenseid;
            this.Signer = historyCertificateDetails.Signer;
            this.RelatedSecurityquestion = new SecurityquestionDTO(historyCertificateDetails.RelatedSecurityquestion);
            this.Securityquestion = historyCertificateDetails.Securityquestion;
            this.Securityanswer = historyCertificateDetails.Securityanswer;
            this.Remarks = historyCertificateDetails.Remarks;
            this.Remarkdesc = historyCertificateDetails.Remarkdesc;
            this.Issuedate = (DateTime)historyCertificateDetails.Issuedate;
            this.Expiredate = (DateTime)historyCertificateDetails.Expiredate;
            this.UpdatedDate = (DateTime)historyCertificateDetails.UpdatedDate;
            this.UpdatedUserName = this.UpdatedUserName;
            this.Customer = new CustomerDTO(historyCertificateDetails.Customer);
        }

        public int Id { get; set; }
        public ProjectDTO Project { get; set; }
        public SubprojectDTO SubProject { get; set; }
        public ExpirationtypeDTO Expire { get; set; }
        public SmartobjectDTO Smartobject { get; set; }
        public CertificatesstatusDTO Certificatesstatus { get; set; }
        public SecurityquestionDTO RelatedSecurityquestion { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DocstypeDTO Docstype { get; set; }
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
        public DateTime Issuedate { get; set; }
        public DateTime Expiredate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedUserName { get; set; }
        public CustomerDTO Customer { get; set; }
    }
}
