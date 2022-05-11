using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Certificateshistory
    {
        public int Id { get; set; }
        [Column(TypeName = "float")]
        public int? Customerid { get; set; }
        public virtual Customer RelatedCustomer { get; set; }
        public string Email { get; set; }
        [Column(TypeName = "float")]
        public int Project { get; set; }
        public virtual Project RelatedProject { get; set; }
        [Column(TypeName = "float")]
        public int Subproject { get; set; }
        public virtual Subproject RelatedSubProject { get; set; }
        public string Company { get; set; }
        public string Hpnumber { get; set; }
        public string Hotem { get; set; }
        public DateTime? Issuedate { get; set; }
        [Column(TypeName = "float")]
        public int? Expire { get; set; }
        public virtual Expirationtype? RelatedExpiration { get; set; }
        public DateTime? Expiredate { get; set; }
        [Column(TypeName = "float")]
        public int Certificatestatus { get; set; }
        public virtual Certificatesstatus RelatedCertificateStatus { get; set; }
        [Column(TypeName = "float")]
        public int? Smartobject { get; set; }
        public virtual Smartobject RelatedSmartObject {get; set;}
        public string Remarksdesc { get; set; }
        [Column(TypeName = "float")]
        public int? Securityquestion { get; set; }
        public virtual Securityquestion RelatedSecurityQuestion { get; set; }
        public string Securityansware { get; set; }
        [Column(TypeName = "float")]
        public int? Docstype { get; set; }
        public virtual Docstype RelatedDocsType { get; set; }
        public string Passportid { get; set; }
        public string Licenceid { get; set; }
        public string Identify { get; set; }
        public string Certificateissuer { get; set; }
        public string Issuerplace { get; set; }
        [Column(TypeName = "float")]
        public int? Certificateid { get; set; }
        public Certificate RelatedCertificate { get; set; }
        [Column(TypeName = "float")]
        public int? Updateduserid { get; set; }
        public virtual Buuser RelatedUser { get; set; }
        public DateTime? Updateddate { get; set; }
        public string Remarks { get; set; }
    }
}
