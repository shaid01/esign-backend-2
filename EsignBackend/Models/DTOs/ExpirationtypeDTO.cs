using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EsignBackend.Models.DTOs
{
    public class ExpirationtypeDTO
    {
        public ExpirationtypeDTO(Expirationtype expire)
        {
            if (expire == null)
                return;

            Id = expire.Id;
            var title = expire.Title != null ? HttpUtility.HtmlDecode(expire.Title) : String.Empty;
            Title = title;
        }
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
