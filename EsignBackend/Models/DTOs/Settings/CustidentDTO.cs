using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EsignBackend.Models.DTOs.Settings
{
    public class CustidentDTO
    {
        public CustidentDTO(Custident custident)
        {
            if (custident == null)
                return;

            Id = custident.Id;
            var title = custident.Title != null ? HttpUtility.HtmlDecode(custident.Title) : String.Empty;
            Title = title;
            Active = custident.Active;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Active { get; set; }
    }
}
