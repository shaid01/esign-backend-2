using EsignBackend.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Validation
{
    public class CertificatesValidator : AbstractValidator<Certificate>
    {
        public CertificatesValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("המספר הסידורי של התעודה חסר");
            RuleFor(x => x.Email).EmailAddress().WithMessage("כתובת מייל לא תקינה");
            RuleFor(x => x.Expiredate).NotEmpty().WithMessage("תאריך תפוגה לא קיים");
            RuleFor(x => x.Issuedate).NotEmpty().WithMessage("תאריך הנפקה לא קיים");
            RuleFor(x => x.Company).NotEmpty().WithMessage("שם חברה לא קיים");
            RuleFor(x => x.ProjectId).NotEmpty().WithMessage("פרוייקט לא קיים");
            RuleFor(x => x.SmartobjectId).NotEmpty().WithMessage("רכיב חכם לא קיים");
            RuleFor(x => x.SubprojectId).NotEmpty().WithMessage("תת פרוייקט לא קיים");
            RuleFor(x => x.CertificatestatusId).NotEmpty().WithMessage("סטטוס תעודה לא קיים");
            RuleFor(x => x.DocstypeId).NotEmpty().WithMessage("Identification document is missing");
            RuleFor(x => x.SecurityquestionId).NotEmpty().WithMessage("חסרה שאלת אבטחה");
            RuleFor(x => x.Securityansware).NotEmpty().WithMessage("תשובה לשאלת אבטחה חסרה");
            RuleFor(x => x.IssuerplaceId).NotEmpty().When(x=> x.CertificatestatusId !=7 && x.CertificatestatusId !=11).WithMessage("מיקום הנפקה חסר");
            RuleFor(x => x.CertificateissuerId).NotEmpty().WithMessage("מנפיק תעודה חסר");
            RuleFor(x => x.Identify).NotEmpty().WithMessage("מזהה הלקוח חסר");
            RuleFor(x => x.LicenceId).Matches(@"^[0-9]*$").WithMessage("מספר רישיון לא תקין");
            RuleFor(x => x.Hpnumber).Matches(@"^[0-9]*$").WithMessage("מספר ח.פ. לא תקין");
        }
    }
}
