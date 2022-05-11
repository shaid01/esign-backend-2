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
            Title = HttpUtility.HtmlDecode(certificateIssuer.Title);
            Active = certificateIssuer.Active;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Active { get; set; }
    }
}
