using System;
using System.Collections.Generic;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Progressreport
    {
        public int Id { get; set; }
        public double? Ticketid { get; set; }
        public string Description { get; set; }
        public double? Updateduserid { get; set; }
        public DateTime? Updateddate { get; set; }
    }
}
