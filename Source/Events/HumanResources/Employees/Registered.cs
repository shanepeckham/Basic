using doLittle.Events;

namespace Events.HumanResources.Employees
{
    public class Registered : Event
    {
        public Registered(EventSourceId eventSourceId) : base(eventSourceId) { }

        public string SocialSecurityNumber { get; set; }
        public string FirstName { get; set;  }
        public string LastName { get; set;  }
    }
}
