using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models
{
    public class CertificateDetails
    {
        public int Id { get; set; }
        public Project Project { get; set; }
        public Subproject SubProject { get; set; }
        public Expirationtype Expire { get; set; }
        public Smartobject Smartobject { get; set; }
        public Certificatesstatus Certificatesstatus { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }

        public Docstype Docstype { get; set; }
        public Isscert CertificateIssuer { get; set; }
        public Issplace CertificateLocation { get; set; }
        public string CustomerIdentifier { get; set; }
        public int CustomerIdentifierId { get; set; }

        public string Company { get; set; }
        public string Hpnumber { get; set; }
        public string Email { get; set; }
        public string Passportid { get; set; }
        public string Licenseid { get; set; }
        public string Signer { get; set; }
        public double Securityquestion { get; set; }
        public string Securityanswer { get; set; }
        public string Remarks { get; set; }
        public string Remarkdesc { get; set; }
        public string Job { get; set; }
        public DateTime Issuedate { get; set; }
        public DateTime Expiredate { get; set; }
    }
}
