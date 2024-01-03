using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Web;

namespace EsignBackend.Models.DTOs
{
    public class DocstypeDTO
    {
        public DocstypeDTO(DocsType docstype)
        {
            if (docstype == null)
                return;
            Id = docstype.Id;
            var title = docstype.Title != null ? HttpUtility.HtmlDecode(docstype.Title) : String.Empty;
            Title = title;
        }
        public int Id { get; set; }
        public string Title { get; set; }

    }
}
