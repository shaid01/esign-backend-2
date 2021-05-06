using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models.DTOs
{
    public class IssplaceDTO
    {
        public IssplaceDTO(Issplace certificateLocation)
        {
            if (certificateLocation == null)
                return;

            Id = certificateLocation.Id;
            Title = certificateLocation.Title;
            Active = certificateLocation.Active;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Active { get; set; }
    }
}
