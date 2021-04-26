using System;
using System.Collections.Generic;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Busetting
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Explain1 { get; set; }
        public string Description { get; set; }
        public double? Priority { get; set; }
        public int? Parentid { get; set; }
    }
}
