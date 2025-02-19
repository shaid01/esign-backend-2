using EsignBackend.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Validation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Idnumber).NotEmpty().WithMessage("תעודת זהות לא תקינה").Matches(@"^[0-9]*$").WithMessage("תעודת זהות לא תקינה");
            //RuleFor(x => x.Email).EmailAddress().WithMessage("כתובת מייל לא תקינה");
            RuleFor(e => e.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("כתובת מייל לא תקינה");
            RuleFor(x => x.Firstname).NotEmpty().WithMessage("שם פרטי לא הוזן");
            RuleFor(x => x.Lastname).NotEmpty().WithMessage("שם משפחה לא הוזן");
            RuleFor(x => x.Phone1).Matches(@"^([0-9-]+$)*").WithMessage("מספר הטלפון יכול להכיל רק מספרים");
            RuleFor(x => x.Mobile1).Matches(@"^([0-9-]+$)*").WithMessage("מספר הטלפון יכול להכיל רק מספרים");
        }
    }
}