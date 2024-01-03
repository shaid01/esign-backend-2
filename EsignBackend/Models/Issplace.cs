using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace EsignBackend.Models
{
    public partial class IssPlace
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }

    }
}
