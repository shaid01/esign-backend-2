using System;
using System.Collections.Generic;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Certificate
    {
        public int Id { get; set; }
        public int Project { get; set; }
        public virtual Project RelatedProject { get; set; }
        public string Company { get; set; }
        public string Hpnumber { get; set; }
        public string Email { get; set; }
        public DateTime? Issuedate { get; set; }
        public DateTime? Expiredate { get; set; }
        public int? Expire { get; set; }
        public virtual Expirationtype? RelatedExpiration { get; set; }
        public int? Smartobject { get; set; }
        public virtual Smartobject? RelatedSmartObject { get; set; }
        public int? Certificatestatus { get; set; }
        public virtual Certificatesstatus? RelatedCertificatesstatus { get; set; }
        public int? Customerid { get; set; }
        public virtual Customer RelatedCustomer { get; set; }
        public int? Subproject { get; set; }
        public virtual Subproject RelatedSubProject { get; set; }
        public int? Docstype { get; set; }
        public virtual Docstype RelatedDocsType { get; set; }
        public string Passportid { get; set; }
        public string Licenceid { get; set; }
        public string Hotem { get; set; }
        public int? Securityquestion { get; set; }
        public virtual Securityquestion RelatedSecurityquestion { get; set; }
        public string Securityansware { get; set; }
        public string Remarksdesc { get; set; }
        public string Job { get; set; }
        public int? Identify { get; set; }
        public virtual Custident? RelatedCustomerIdentifier { get; set; }
        public int? Certificateissuer { get; set; }
        public virtual Isscert? RelatedCertificateissuer { get; set; }
        public int? Issuerplace { get; set; }
        public virtual Issplace? RelatedIssuerPlace { get; set; }
        public string Remarks { get; set; }
        public virtual ICollection<Certificateshistory> HistoryCertificates { get; set; }
    }
}
