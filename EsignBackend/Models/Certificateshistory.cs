using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace EsignBackend.Models
{
    public partial class CertificatesHistory
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer RelatedCustomer { get; set; }
        public string Email { get; set; }
        public int ProjectId { get; set; }
        public virtual Project RelatedProject { get; set; }
        public int SubprojectId { get; set; }
        public virtual SubProject RelatedSubProject { get; set; }
        public string Company { get; set; }
        public string Hpnumber { get; set; }
        public string Hotem { get; set; }
        public DateTime? Issuedate { get; set; }
        public int? ExpirationtypeId { get; set; }
        public virtual ExpirationType? RelatedExpiration { get; set; }
        public DateTime? Expiredate { get; set; }
        public int CertificatestatusId { get; set; }
        public virtual CertificatesStatus RelatedCertificateStatus { get; set; }
        public int? SmartobjectId { get; set; }
        public virtual SmartObject RelatedSmartObject {get; set;}
        public string Remarksdesc { get; set; }
        public int? SecurityquestionId { get; set; }
        public virtual SecurityGuestion RelatedSecurityQuestion { get; set; }
        public string Securityansware { get; set; }
        public int? DocstypeId { get; set; }
        public virtual DocsType RelatedDocsType { get; set; }
        public string Passportid { get; set; }
        public string Licenceid { get; set; }
        public string Identify { get; set; }
        public int? CertificateissuerId { get; set; }
        public int? IssuerplaceId { get; set; }
        public int? CertificateId { get; set; }
        public Certificate RelatedCertificate { get; set; }
        public int? UpdateduserId { get; set; }
        public virtual BuUser RelatedUser { get; set; }
        public DateTime? Updateddate { get; set; }
        public string Remarks { get; set; }
    }
}
