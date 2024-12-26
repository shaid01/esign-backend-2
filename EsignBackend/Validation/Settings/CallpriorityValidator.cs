using EsignBackend.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Validation.Settings
{
    public class CallpriorityValidator : AbstractValidator<Callpriority>
    {
        public CallpriorityValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("שם עדיפות הפניה לא קיים");
        }
    }
}
