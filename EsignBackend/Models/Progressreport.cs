using System;
using System.Collections.Generic;

#nullable disable

namespace EsignBackend.Models
{
    public partial class ProgressReport
    {
        public int Id { get; set; }
        public int? TicketId { get; set; }
        public string Description { get; set; }
        public int? UpdateduserId { get; set; }
        public DateTime? Updateddate { get; set; }
    }
}