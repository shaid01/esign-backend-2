using System;
using System.Collections.Generic;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public double? Departmantid { get; set; }
        public double? Status { get; set; }
        public double? Priorityd { get; set; }
        public double? Updateduserid { get; set; }
        public DateTime? Updateddate { get; set; }
        public double? Transferto { get; set; }
        public DateTime? Schedule { get; set; }
        public double? Lastupdater { get; set; }
        public string Lastdescription { get; set; }
        public double? Calleruserid { get; set; }
        public DateTime? Donedate { get; set; }
        public string Closerequest { get; set; }
        public DateTime? Duedate { get; set; }
        public string Ticketname { get; set; }
        public string Comefrom { get; set; }
        public string Sulution { get; set; }
        public int? ProjectId { get; set; }
    }
}
