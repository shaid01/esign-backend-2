using System.Web;

namespace EsignBackend.Models.DTOs
{
    public class DocstypeDTO
    {
        public DocstypeDTO(Docstype docstype)
        {
            if (docstype == null)
                return;
            Id = docstype.Id;
            Title = HttpUtility.HtmlDecode(docstype.Title); 
        }
        public int Id { get; set; }
        public string Title { get; set; }

    }
}
