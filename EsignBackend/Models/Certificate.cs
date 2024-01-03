using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Certificate
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public virtual Project RelatedProject { get; set; }
        public string Company { get; set; }
        public string Hpnumber { get; set; }
        public string Email { get; set; }
        public DateTime? Issuedate { get; set; }
        public DateTime? Expiredate { get; set; }
        public int? ExpirationtypeId { get; set; }
        public virtual ExpirationType? RelatedExpiration { get; set; }
        public int? SmartobjectId { get; set; }
        public virtual SmartObject? RelatedSmartObject { get; set; }
        public int? CertificatestatusId { get; set; }
        public virtual CertificatesStatus? RelatedCertificatesstatus { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer RelatedCustomer { get; set; }
        public int? SubprojectId { get; set; }
        public virtual SubProject RelatedSubProject { get; set; }
        public int? DocstypeId { get; set; }
        public virtual DocsType RelatedDocsType { get; set; }
        public string PassportId { get; set; }
        public string LicenceId { get; set; }
        public string Hotem { get; set; }
        public int? SecurityquestionId { get; set; }
        public virtual SecurityGuestion RelatedSecurityquestion { get; set; }
        public string Securityansware { get; set; }
        public string Remarksdesc { get; set; }
        public string Job { get; set; }
        public int? Identify { get; set; }
        public virtual Custident? RelatedCustomerIdentifier { get; set; }
        public int? CertificateissuerId { get; set; }
        public virtual Isscert? RelatedCertificateissuer { get; set; }
        public int? IssuerplaceId { get; set; }
        public virtual IssPlace? RelatedIssuerPlace { get; set; }
        public string Remarks { get; set; }
        public string AttorneyLicenseNumber { get; set; }
        public virtual ICollection<CertificatesHistory> HistoryCertificates { get; set; }
    }
}
