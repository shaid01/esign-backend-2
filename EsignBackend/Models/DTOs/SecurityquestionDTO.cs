using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models.DTOs
{
    public class SecurityquestionDTO
    {

        public SecurityquestionDTO(Securityquestion securityquestion)
        {
            if (securityquestion == null)
                return;
            Id = securityquestion.Id;
            Title = securityquestion.Title;
        }
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
