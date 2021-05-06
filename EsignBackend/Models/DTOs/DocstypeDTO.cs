using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models.DTOs
{
    public class DocstypeDTO
    {
        public DocstypeDTO(Docstype docstype)
        {
            if (docstype == null)
                return;
            Id = docstype.Id;
            Title = docstype.Title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
       
    }
}
