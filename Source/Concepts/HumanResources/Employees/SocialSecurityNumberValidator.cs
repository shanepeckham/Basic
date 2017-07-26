using doLittle.FluentValidation;
using FluentValidation;

namespace Concepts.HumanResources.Employees
{
    public class SocialSecurityNumberValidator : InputValidator<SocialSecurityNumber>
    {
        public SocialSecurityNumberValidator()
        {
            RuleFor(s => s.Value)
                .Length(11)
                    .WithMessage("Social security number must have a length of 11")
                .NotEmpty()
                    .WithMessage("Social Security Number is required");
        }
    }
}