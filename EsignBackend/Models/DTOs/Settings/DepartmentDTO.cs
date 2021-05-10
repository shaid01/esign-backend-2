using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models.DTOs.Settings
{
    public class DepartmentDTO
    {
        public DepartmentDTO(Departmant departmant)
        {
            if (departmant == null)
                return;

            Id = departmant.Id;
            Title = departmant.Title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
}
