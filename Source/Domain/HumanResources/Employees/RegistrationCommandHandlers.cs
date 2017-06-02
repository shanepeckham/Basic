using doLittle.Commands;
using doLittle.Domain;

namespace Domain.HumanResources.Employees
{
    public class RegistrationCommandHandlers : IHandleCommands
    {
        IAggregateRootRepository<Registration> _repository;

        public RegistrationCommandHandlers(IAggregateRootRepository<Registration> repository)
        {
            _repository = repository;
        }

        public void Handle(Register command)
        {
            var registration = _repository.Get(command.EmployeeId);
            registration.Register(command.SocialSecurityNumber, command.FirstName, command.LastName);
        }
    }
}