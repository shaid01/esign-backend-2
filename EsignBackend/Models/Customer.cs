using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Idnumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone1 { get; set; }
        public string Mobile1 { get; set; }
        public int Securityquestion { get; set; }
        public virtual Securityquestion RelatedSecurityquestion { get; set; }
        public string Securityansware { get; set; }
        public string Certificates { get; set; }
        public string Temp { get; set; }
        public double? Calleruserid { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public virtual ICollection<Certificate> CustomerCertificates { get; set; }
        public virtual ICollection<Certificateshistory> CustomerHistoryCertificates { get; set; }
    }
}