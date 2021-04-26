using System;
using System.Collections.Generic;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Subproject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double? Project { get; set; }
    }
}
