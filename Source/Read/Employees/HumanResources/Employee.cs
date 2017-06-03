using System;
using doLittle.Read;

namespace Read.HumanResources.Employees
{
    public class Employee : IReadModel
    {
        public Guid Id { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string FirstName { get; set;  }
        public string LastName { get; set; }
    }
}