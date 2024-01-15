using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models.DTOs.Settings
{
    public class DepartmentDTO
    {
        public DepartmentDTO(Department department)
        {
            if (department == null)
                return;

            Id = department.Id;
            Title = department.Title;
        }
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
