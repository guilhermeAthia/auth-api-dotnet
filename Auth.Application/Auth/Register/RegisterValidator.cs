using FluentValidation;

namespace Auth.Application.Auth.Register;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required");
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required");
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required");
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required");
    }
    
}