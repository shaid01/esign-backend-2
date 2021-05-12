using System;
using System.Collections.Generic;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Certificateshistory
    {
        public int Id { get; set; }
        public int Customerid { get; set; }
        public virtual Customer RelatedCustomer { get; set; }
        public string Email { get; set; }
        public int Project { get; set; }
        public virtual Project RelatedProject { get; set; }
        public int Subproject { get; set; }
        public virtual Subproject RelatedSubProject { get; set; }
        public string Company { get; set; }
        public string Hpnumber { get; set; }
        public string Hotem { get; set; }
        public DateTime? Issuedate { get; set; }
        public int? Expire { get; set; }
        public virtual Expirationtype? RelatedExpiration { get; set; }
        public DateTime? Expiredate { get; set; }
        public int Certificatestatus { get; set; }
        public virtual Certificatesstatus RelatedCertificateStatus { get; set; }
        public int? Smartobject { get; set; }
        public virtual Smartobject RelatedSmartObject {get; set;}
        public string Remarksdesc { get; set; }
        public int? Securityquestion { get; set; }
        public virtual Securityquestion RelatedSecurityQuestion { get; set; }
        public string Securityansware { get; set; }
        public int? Docstype { get; set; }
        public virtual Docstype RelatedDocsType { get; set; }
        public string Passportid { get; set; }
        public string Licenceid { get; set; }
        public string Identify { get; set; }
        public string Certificateissuer { get; set; }
        public string Issuerplace { get; set; }
        public int? Certificateid { get; set; }
        public Certificate RelatedCertificate { get; set; }
        public int? Updateduserid { get; set; }
        public virtual Buuser RelatedUser { get; set; }
        public DateTime? Updateddate { get; set; }
        public string Remarks { get; set; }
    }
}
