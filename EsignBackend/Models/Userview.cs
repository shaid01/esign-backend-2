using System;
using System.Collections.Generic;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Userview
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Sessionvalues { get; set; }
        public int? Userid { get; set; }
    }
}
