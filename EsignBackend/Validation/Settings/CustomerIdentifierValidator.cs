using EsignBackend.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Validation.Settings
{
    public class CustomerIdentifierValidator : AbstractValidator<Custident>
    {
        public CustomerIdentifierValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("שם מזהה הלקוח לא קיים");
            //int value = 0;
            //RuleFor(x => x.Id.ToString()).Must(x => int.TryParse(x, out value)).WithMessage("מספר סידורי לא תקין");
            RuleFor(x => x.Active).Matches("^[a-zA-Z]{1,2}$").WithMessage("סטטוס יכול להכיל עד 2 אותיות באנגלית");
        }
    }
}
