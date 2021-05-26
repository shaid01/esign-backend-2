using System;
using System.Collections.Generic;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Expirationtype
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<Certificateshistory> HistoryCertificates { get; set; }
    }
}
