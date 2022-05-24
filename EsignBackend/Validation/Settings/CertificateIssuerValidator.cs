using EsignBackend.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Validation.Settings
{
    public class CertificateIssuerValidator : AbstractValidator<Isscert>
    {
        public CertificateIssuerValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("שם מנפיק התעודה לא קיים");
            int value = 0;
            RuleFor(x => x.Id.ToString()).Must(x => int.TryParse(x, out value)).WithMessage("מספר סידורי לא תקין");
            RuleFor(x => x.Active).Matches("^[a-zA-Z]{0,2}$").WithMessage(" סטטוס יכול להכיל עד 2 אותיות באנגלית");
        }
    }
}