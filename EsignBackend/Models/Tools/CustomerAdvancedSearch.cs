using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models.Tools
{
    public class CustomerAdvancedSearch
    {
        public string? CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Company { get; set; }
        public string? Phone { get; set; }

    }
}
