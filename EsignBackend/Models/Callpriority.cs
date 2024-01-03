using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace EsignBackend.Models
{
    public partial class CallPriority
    {        
        public int Id { get; set; }
        public string Title { get; set; }
    }
}