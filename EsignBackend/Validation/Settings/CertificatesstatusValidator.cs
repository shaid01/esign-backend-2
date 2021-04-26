using EsignBackend.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Validation.Settings
{
    public class CertificatesstatusValidator : AbstractValidator<Certificatesstatus>
    {
        public CertificatesstatusValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("שם סטטוס התעודה לא קיים");
            int value = 0;
            RuleFor(x => x.Id.ToString()).Must(x => int.TryParse(x, out value)).WithMessage("מספר סידורי לא תקין");
        }
    }
}
