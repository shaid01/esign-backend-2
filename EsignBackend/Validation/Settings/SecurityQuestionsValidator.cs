using EsignBackend.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Validation.Settings
{
    public class SecurityQuestionsValidator : AbstractValidator<Securityquestion>
    {
        public SecurityQuestionsValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("שם השאלה לא קיים");
            int value = 0;
            RuleFor(x => x.Id.ToString()).Must(x => int.TryParse(x, out value)).WithMessage("מספר סידורי לא תקין");
        }
    }
}
