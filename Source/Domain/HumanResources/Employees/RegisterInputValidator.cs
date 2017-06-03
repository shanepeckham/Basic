using doLittle.Commands;
using doLittle.FluentValidation.Commands;
using doLittle.Security;
using FluentValidation;

namespace Domain.HumanResources.Employees
{
    public class RegisterInputValidator : CommandInputValidator<Register>
    {
        public RegisterInputValidator()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty()
                    .WithMessage("FirstName is required");

            RuleFor(r => r.LastName)
                .NotEmpty()
                    .WithMessage("LastName is required");
        }
    }

    /*

    public class SecurityDescriptor : BaseSecurityDescriptor
    {
        public SecurityDescriptor()
        {

            When.Handling().Commands().InNamespace(typeof(RegisterInputValidator).Namespace, s => s.User().MustBeInRole("Xomething"));

        }
           
    }
    */
}
