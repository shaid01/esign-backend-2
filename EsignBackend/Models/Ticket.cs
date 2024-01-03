using System;
using System.Collections.Generic;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public int? Departmantid { get; set; }
        public int? Status { get; set; }
        public int? Priorityd { get; set; }
        public int? UpdateduserId { get; set; }
        public DateTime? Updateddate { get; set; }
        public int? Transferto { get; set; }
        public DateTime? Schedule { get; set; }
        public int? Lastupdater { get; set; }
        public string Lastdescription { get; set; }
        public int? CalleruserId { get; set; }
        public DateTime? Donedate { get; set; }
        public string Closerequest { get; set; }
        public DateTime? Duedate { get; set; }
        public string Ticketname { get; set; }
        public string Comefrom { get; set; }
        public string Sulution { get; set; }
        public int? ProjectId { get; set; }
    }
}
