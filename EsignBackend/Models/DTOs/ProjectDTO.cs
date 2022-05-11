using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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
            Title = HttpUtility.HtmlDecode(project.Title);
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
}
