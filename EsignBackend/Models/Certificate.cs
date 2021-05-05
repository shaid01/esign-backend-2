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
        public double? Expire { get; set; }
        public double? Smartobject { get; set; }
        public double? Certificatestatus { get; set; }
        public double? Customerid { get; set; }
        public int? Subproject { get; set; }
        public virtual Subproject RelatedSubProject { get; set; }

        public double? Docstype { get; set; }
        public string Passportid { get; set; }
        public string Licenceid { get; set; }
        public string Hotem { get; set; }
        public double? Securityquestion { get; set; }
        public string Securityansware { get; set; }
        public string Remarksdesc { get; set; }
        public string Job { get; set; }
        public double? Identify { get; set; }
        public int? Certificateissuer { get; set; }
        public virtual Isscert? RelatedCertificateissuer { get; set; }

        public int? Issuerplace { get; set; }
        public virtual Issplace? RelatedIssuerPlace { get; set; }
        public string Remarks { get; set; }
    }
}
