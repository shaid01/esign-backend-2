using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models.DTOs.Settings
{
    public class CustidentDTO
    {
        public CustidentDTO(Custident custident)
        {
            if (custident == null)
                return;

            Id = custident.Id;
            Title = custident.Title;
            Active = custident.Active;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Active { get; set; }
    }
}
