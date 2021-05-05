using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Issplace
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Active { get; set; }
    }
}
