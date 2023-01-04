using System.Composition;
using System;
using System.Web;

namespace EsignBackend.Models.DTOs
{
    public class IsscertDTO
    {
        public IsscertDTO(Isscert certificateIssuer)
        {
            if (certificateIssuer == null)
                return;
            Id = certificateIssuer.Id;
            var title = certificateIssuer.Title != null ? HttpUtility.HtmlDecode(certificateIssuer.Title) : String.Empty;
            Title = title;
            Active = certificateIssuer.Active;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Active { get; set; }
    }
}
