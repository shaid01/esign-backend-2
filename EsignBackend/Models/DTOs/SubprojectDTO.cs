using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EsignBackend.Models.DTOs
{
    public class SubprojectDTO
    {
        public SubprojectDTO(Subproject subProject)
        {
            if (subProject == null)
                return;
            Id = subProject.Id;
            var title = subProject.Title != null ? HttpUtility.HtmlDecode(subProject.Title): String.Empty;
            Title = title;
            Project = subProject.ProjectId;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public int Project { get; set; }
    }
}