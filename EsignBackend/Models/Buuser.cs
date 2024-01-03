using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace EsignBackend.Models
{
    public partial class BuUser
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Userlevel { get; set; }
        public string Usergroup { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Pass { get; set; }
        public string Picture { get; set; }
        public int? UpdateduserId { get; set; }
        public DateTime? Updateddate { get; set; }
        public double? Parentid { get; set; }
        public string Permissions { get; set; }
        public string Allowedips { get; set; }
        public DateTime? Expires { get; set; }
        public string Remarks { get; set; }
        public string SessionValues { get; set; }
        public double? Provider { get; set; }
        public double? Elang { get; set; }
        public int? DepartmantId { get; set; }
        public virtual ICollection<CertificatesHistory> HistoryCertificates { get; set; }
        public virtual Department? Department { get; set; }
    }
}
