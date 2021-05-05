using System;
using System.Collections.Generic;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Isscert
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Active { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }

    }
}
