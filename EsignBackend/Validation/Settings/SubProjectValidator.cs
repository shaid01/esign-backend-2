using EsignBackend.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Validation.Settings
{
    public class SubProjectValidator : AbstractValidator <Subproject>
    {
        public SubProjectValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("שם תת הפרוייקט לא קיים");
            RuleFor(x => x.Project).NotEmpty().WithMessage("לא קיים פרוייקט עבור תת הפרוייקט");
        }
    }
}
