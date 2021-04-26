using System;
using System.Collections.Generic;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Certificate
    {
        public int Id { get; set; }
        public double? Project { get; set; }
        public string Company { get; set; }
        public string Hpnumber { get; set; }
        public string Email { get; set; }
        public DateTime? Issuedate { get; set; }
        public DateTime? Expiredate { get; set; }
        public double? Expire { get; set; }
        public double? Smartobject { get; set; }
        public double? Certificatestatus { get; set; }
        public double? Customerid { get; set; }
        public double? Subproject { get; set; }
        public double? Docstype { get; set; }
        public string Passportid { get; set; }
        public string Licenceid { get; set; }
        public string Hotem { get; set; }
        public double? Securityquestion { get; set; }
        public string Securityansware { get; set; }
        public string Remarksdesc { get; set; }
        public string Job { get; set; }
        public double? Identify { get; set; }
        public double? Certificateissuer { get; set; }
        public double? Issuerplace { get; set; }
        public string Remarks { get; set; }
    }
}
