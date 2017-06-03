using doLittle.Events;
using doLittle.Read;
using Events.HumanResources.Employees;

namespace Read.HumanResources.Employees
{
    public class EventProcessor : IProcessEvents
    {
        IReadModelRepositoryFor<Employee> _repository;

        public EventProcessor(IReadModelRepositoryFor<Employee> repository)
        {
            _repository = repository;
        }

        public void Process(Registered @event)
        {
            _repository.Insert(new Employee
            {
                Id = @event.EventSourceId,
                SocialSecurityNumber = @event.SocialSecurityNumber,
                FirstName = @event.FirstName,
                LastName = @event.LastName
            });
        }
    }
}
