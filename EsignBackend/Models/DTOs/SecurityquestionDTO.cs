using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EsignBackend.Models.DTOs
{
    public class SecurityquestionDTO
    {

        public SecurityquestionDTO(SecurityGuestion securityquestion)
        {
            if (securityquestion == null)
                return;
            Id = securityquestion.Id;
            var title = securityquestion.Title != null ? HttpUtility.HtmlDecode(securityquestion.Title) : String.Empty;
            Title = title;
        }
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
