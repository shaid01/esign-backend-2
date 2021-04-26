using EsignBackend.Dtos.Login;
using FluentValidation;


namespace EsignBackend.Validation
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("שם משתמש או סיסמה לא חוקיים").Matches(".{1,30}$").WithMessage("שם משתמש או סיסמה לא חוקיים");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("שם משתמש או סיסמה לא חוקיים");
        }
    }
}