using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models.DTOs
{
    public class ExpirationtypeDTO
    {
        public ExpirationtypeDTO(Expirationtype expire)
        {
            if (expire == null)
                return;

            Id = expire.Id;
            Title = expire.Title;
        }
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
