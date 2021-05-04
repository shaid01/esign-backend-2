using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public Project Project { get; set; }
    }
}
