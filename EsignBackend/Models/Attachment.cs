using System;
using System.Collections.Generic;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Attachment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string File1 { get; set; }
        public int? TicketId { get; set; }
    }
}