using doLittle.Commands;
using doLittle.Domain;

namespace Domain.HumanResources.Employees
{
    public class RegistrationCommandHandler : IHandleCommands
    {
        IAggregateRootRepository<Registration> _repository;
        public RegistrationCommandHandler(IAggregateRootRepository<Registration> repository)
        {
            _repository = repository;
        }

        public void Handle(Register command)
        {
            var registration = _repository.Get(command.Employee);
            registration.Register(command.SocialSecurityNumber, command.FirstName, command.LastName);
        }
    }
}
