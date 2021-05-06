using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models.DTOs
{
    public class ProjectDTO
    {


        public ProjectDTO(Project project)
        {
            if (project == null)
            {
                return;
            }
            Id = project.Id;
            Title = project.Title;
        }

        public int Id { get; set; }
        public string Title { get; set; }        
    }
}
