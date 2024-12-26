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
        }
    }
}
