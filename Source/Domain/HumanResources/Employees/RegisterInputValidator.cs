using doLittle.FluentValidation.Commands;

namespace Domain.HumanResources.Employees
{
    public class RegisterInputValidator : CommandInputValidator<Register>
    {
        public RegisterInputValidator()
        {

        }
    }
}