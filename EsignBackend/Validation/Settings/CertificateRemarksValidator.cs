using EsignBackend.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Validation.Settings
{
    public class CertificateRemarksValidator : AbstractValidator<CertificateRemark>
    {
        public CertificateRemarksValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("שם הערת תעודה לא קיים");
        }
    }
}
