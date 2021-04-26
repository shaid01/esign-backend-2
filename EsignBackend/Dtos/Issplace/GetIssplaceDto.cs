using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Dtos.Issplace
{
    public class GetIssplaceDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Active { get; set; }
    }
}
