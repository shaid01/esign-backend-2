using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models.DTOs.Settings
{
    public class CallpriorityDTO
    {
        public CallpriorityDTO(CallPriority callpriority)
        {
            if (callpriority == null)
                return;

            Id = callpriority.Id;
            Title = callpriority.Title;
        }
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
