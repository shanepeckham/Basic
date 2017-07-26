using System;
using Concepts.HumanResources.Employees;
using doLittle.Commands;

namespace Domain.HumanResources.Employees
{
    public class Register : ICommand
    {
        public Guid Employee { get; set; }
        public SocialSecurityNumber SocialSecurityNumber { get; set; }
        public string FirstName { get; set;  }
        public string LastName { get; set; }
    }
}
