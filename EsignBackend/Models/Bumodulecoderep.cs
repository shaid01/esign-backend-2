using System;
using System.Collections.Generic;

#nullable disable

namespace EsignBackend.Models
{
    public partial class BuModuleCoderep
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? Times { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public int? Tid { get; set; }
        public int? ParentId { get; set; }
        public string Status { get; set; }
        public string Applytoall { get; set; }
    }
}
