using EsignBackend.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Validation.Settings
{
    public class ExpirationTypeValidator : AbstractValidator<Expirationtype>
    {
        public ExpirationTypeValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("סוג תפוגה לא קיים");
        }
    }
}
