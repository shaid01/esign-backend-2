using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models.DTOs.Settings
{
    public class CallStatusDto
    {
        public CallStatusDto(Callstatus callstatus)
        {
            if (callstatus == null)
                return;
            Id = callstatus.Id;
            Title = callstatus.Title;
            Color = callstatus.Color;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
    }
}
