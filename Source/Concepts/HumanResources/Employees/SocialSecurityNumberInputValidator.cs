using doLittle.FluentValidation;
using FluentValidation;

namespace Concepts.HumanResources.Employees
{
    public class SocialSecurityNumberInputValidator : InputValidator<SocialSecurityNumber>
    {
        public SocialSecurityNumberInputValidator()
        {
            RuleFor(s => s.Value).NotEmpty().WithMessage("Social Security Number is required");
        }
    }
}