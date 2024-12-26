using EsignBackend.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Validation.Settings
{
    public class DepartmentsValidator : AbstractValidator<Department>
    {
        public DepartmentsValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("שם המחלקה לא קיים");
        }
    }
}
