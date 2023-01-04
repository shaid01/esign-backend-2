using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EsignBackend.Models.DTOs
{
    public class SmartobjectDTO
    {
        public SmartobjectDTO(Smartobject smartobject)
        {
            if (smartobject == null)
                return;

            Id = smartobject.Id;
            var title = smartobject.Title != null ? HttpUtility.HtmlDecode(smartobject.Title) : String.Empty;
            Title = title;
        }
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
