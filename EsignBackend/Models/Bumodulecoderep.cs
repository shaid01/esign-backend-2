using System;
using System.Collections.Generic;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Bumodulecoderep
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double? Times { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public double? Tid { get; set; }
        public double? Parentid { get; set; }
        public string Status { get; set; }
        public string Applytoall { get; set; }
    }
}
