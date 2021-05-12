using System;
using System.Collections.Generic;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Subproject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Project { get; set; }
        public virtual Project RelatedProject { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<Certificateshistory> HistoryCertificates { get; set; }

    }
}
