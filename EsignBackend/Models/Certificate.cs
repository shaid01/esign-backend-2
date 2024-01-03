using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Certificate
    {
        public int Id { get; set; }
        //[Column(TypeName = "float")]
        public int ProjectId { get; set; }
        public virtual Project RelatedProject { get; set; }
        public string Company { get; set; }
        public string Hpnumber { get; set; }
        public string Email { get; set; }
        public DateTime? Issuedate { get; set; }
        public DateTime? Expiredate { get; set; }
        //[Column(TypeName = "float")]
        public int? Expire { get; set; }
        public virtual Expirationtype? RelatedExpiration { get; set; }
        //[Column(TypeName = "float")]
        public int? SmartobjectId { get; set; }
        public virtual Smartobject? RelatedSmartObject { get; set; }
        //[Column(TypeName = "float")]
        public int? CertificatestatusId { get; set; }
        public virtual Certificatesstatus? RelatedCertificatesstatus { get; set; }
        //[Column(TypeName = "float")]
        public int? CustomerId { get; set; }
        public virtual Customer RelatedCustomer { get; set; }
        //[Column(TypeName = "float")]
        public int? SubprojectId { get; set; }
        public virtual Subproject RelatedSubProject { get; set; }
        //[Column(TypeName = "float")]
        public int? DocstypeId { get; set; }
        public virtual Docstype RelatedDocsType { get; set; }
        public string PassportId { get; set; }
        public string LicenceId { get; set; }
        public string Hotem { get; set; }
        //[Column(TypeName = "float")]
        public int? SecurityquestionId { get; set; }
        public virtual Securityquestion RelatedSecurityquestion { get; set; }
        public string Securityansware { get; set; }
        public string Remarksdesc { get; set; }
        public string Job { get; set; }
        //[Column(TypeName = "float")]
        public int? Identify { get; set; }
        public virtual Custident? RelatedCustomerIdentifier { get; set; }
        //[Column(TypeName = "float")]
        public int? CertificateissuerId { get; set; }
        public virtual Isscert? RelatedCertificateissuer { get; set; }
        //[Column(TypeName = "float")]
        public int? IssuerplaceId { get; set; }
        public virtual Issplace? RelatedIssuerPlace { get; set; }
        public string Remarks { get; set; }
        public string AttorneyLicenseNumber { get; set; }
        public virtual ICollection<Certificateshistory> HistoryCertificates { get; set; }
    }
}
