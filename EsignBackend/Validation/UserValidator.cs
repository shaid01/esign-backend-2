using EsignBackend.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EsignBackend.Validation
{
    public class UserValidator : AbstractValidator<Buuser>
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
            //08-DEC-2024
            RuleFor(x => x.Usergroup).NotEmpty().Must(BeAValidUserGroup).WithMessage("המשתמש חייב להשתייך לאחד מהקבוצות הבאות: 'מנהל', 'מנפיק' או 'תומך'");
        }

        private bool BeAValidUserGroup(string userGroup)
        {
            if (string.IsNullOrWhiteSpace(userGroup))
            {
                return false;
            }

            var regex1 = new Regex(@"\u05DE\u05E0\u05D4\u05DC"); //מנהל
            var regex2 = new Regex(@"\u05DE\u05E0\u05E4\u05D9\u05E7"); //מנפיק
            var regex3 = new Regex(@"\u05EA\u05D5\u05DE\u05DA"); //תומך

            bool result = regex1.IsMatch(userGroup);

            if(!result)
            {
                result = regex2.IsMatch(userGroup);

                if (!result)
                {
                    result = regex3.IsMatch(userGroup);
                }
            }

            return result;
        }
    }
}
