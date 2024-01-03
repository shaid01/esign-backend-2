using System;
using System.Collections.Generic;

#nullable disable

namespace EsignBackend.Models
{
    public partial class BuDesign
    {
        public int Id { get; set; }
        public string Lable { get; set; }
        public string Fieldname { get; set; }
        public string Description { get; set; }
        public string Value1 { get; set; }
        public string File1 { get; set; }
    }
}
