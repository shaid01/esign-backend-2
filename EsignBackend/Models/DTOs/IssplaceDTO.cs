using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EsignBackend.Models.DTOs
{
    public class IssplaceDTO
    {
        public IssplaceDTO(IssPlace certificateLocation)
        {
            if (certificateLocation == null)
                return;

            Id = certificateLocation.Id;
            var title = certificateLocation.Title != null ? HttpUtility.HtmlDecode(certificateLocation.Title) : String.Empty;
            Title = title;
            Active = certificateLocation.Active;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Active { get; set; }
    }
}
