using EsignBackend.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Validation.Settings
{
    public class CallStatusValidator :  AbstractValidator<Callstatus>
    {
        public CallStatusValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("שם סטטוס הפניה לא קיים");
        }
    }
}
