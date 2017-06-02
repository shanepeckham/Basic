using doLittle.Domain;
using doLittle.Events;

namespace Domain.HumanResources.Employees
{
    public class Registration : AggregateRoot
    {
        public Registration(EventSourceId id) : base(id) {}


        public void Register(string socialSecurityNumber, string firstName, string lastName)
        {
            

        }
       
    }
}