using EsignBackend.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Validation.Settings
{
    public class ProjectsValidator : AbstractValidator<Project>
    {
        public ProjectsValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("שם הפרוייקט לא קיים");
        }
    }
}
