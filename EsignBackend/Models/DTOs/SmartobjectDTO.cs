using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models.DTOs
{
    public class SmartobjectDTO
    {
        public SmartobjectDTO(Smartobject smartobject)
        {
            if (smartobject == null)
                return;

            Id = smartobject.Id;
            Title = smartobject.Title;
        }
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
