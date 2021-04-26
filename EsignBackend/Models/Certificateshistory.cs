using System;
using System.Collections.Generic;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Certificateshistory
    {
        public int Id { get; set; }
        public double? Customerid { get; set; }
        public string Email { get; set; }
        public double? Project { get; set; }
        public double? Subproject { get; set; }
        public string Company { get; set; }
        public string Hpnumber { get; set; }
        public string Hotem { get; set; }
        public DateTime? Issuedate { get; set; }
        public double? Expire { get; set; }
        public DateTime? Expiredate { get; set; }
        public double? Certificatestatus { get; set; }
        public double? Smartobject { get; set; }
        public string Remarksdesc { get; set; }
        public double? Securityquestion { get; set; }
        public string Securityansware { get; set; }
        public double? Docstype { get; set; }
        public string Passportid { get; set; }
        public string Licenceid { get; set; }
        public string Identify { get; set; }
        public string Certificateissuer { get; set; }
        public string Issuerplace { get; set; }
        public double? Certificateid { get; set; }
        public double? Updateduserid { get; set; }
        public DateTime? Updateddate { get; set; }
        public string Remarks { get; set; }
    }
}
