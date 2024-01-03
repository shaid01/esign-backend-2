using EsignBackend.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Validation
{
    public class UserValidator : AbstractValidator<BuUser>
    {
        public UserValidator()
        {
           // RuleFor(x => x.Id).NotEmpty().WithMessage("User id is missing");
            RuleFor(x => x.Username).NotEmpty().WithMessage("שם המשתמש לא הוזן").Matches("^[A-Za-z0-9]$*").WithMessage("שם המשתמש יכול להכיל רק אותיות באנגלית ומספרים").Matches("^[^# “”]*$").WithMessage("הוכנסו תווים לא חוקיים לשם המשתמש");
            RuleFor(x => x.Email).EmailAddress().WithMessage("כתובת אימייל אינה תקינה");
            RuleFor(x => x.Firstname).NotEmpty().WithMessage("שם פרטי לא הוזן");
            RuleFor(x => x.Lastname).NotEmpty().WithMessage("שם משפחה לא הוזן");
            RuleFor(x => x.Expires).NotEmpty().WithMessage("תאריך תפוגה לא הוזן");
            RuleFor(x => x.Phone).Matches(@"^[0-9-]+$").WithMessage("מספר הטלפון יכול להכיל רק מספרים");
        }
    }
}
