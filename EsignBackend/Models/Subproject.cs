using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace EsignBackend.Models
{
    public partial class SubProject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProjectId { get; set; }
        public virtual Project RelatedProject { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<CertificatesHistory> HistoryCertificates { get; set; }

    }
}