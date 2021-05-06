using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models.DTOs
{
    public class IsscertDTO
    {
        public IsscertDTO(Isscert certificateIssuer)
        {
            if (certificateIssuer == null)
                return;
            Id = certificateIssuer.Id;
            Title = certificateIssuer.Title;
            Active = certificateIssuer.Active;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Active { get; set; }
    }
}
