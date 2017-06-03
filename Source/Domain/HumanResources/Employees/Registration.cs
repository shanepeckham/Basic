using doLittle.Domain;
using doLittle.Events;
using Events.HumanResources.Employees;

namespace Domain.HumanResources.Employees
{
    public class Registration : AggregateRoot
    {
        public Registration(EventSourceId eventSourceId) : base(eventSourceId) { }

        public void Register(string ssn, string firstName, string lastName)
        {
            Apply(new Registered(EventSourceId)
            {
                SocialSecurityNumber = ssn,
                FirstName = firstName,
                LastName = lastName
            });
        }

        /*
        void On(Registered @event)
        {


        }
        */
    }
}
