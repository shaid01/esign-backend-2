using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EsignBackend.Models.DTOs
{
    public class CertificatesstatusDTO
    {
        public CertificatesstatusDTO(Certificatesstatus certificatesstatus)
        {
            if (certificatesstatus == null)
                return;

            Id = certificatesstatus.Id;
            var title = certificatesstatus.Title != null ? HttpUtility.HtmlDecode(certificatesstatus.Title) : String.Empty;
            Title = title;
            Color = certificatesstatus.Color;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
    }
}
