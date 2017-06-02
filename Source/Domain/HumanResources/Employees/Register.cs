using System;
using doLittle.Commands;
using Concepts.HumanResources.Employees;

namespace Domain.HumanResources.Employees
{
    public class Register : ICommand
    {
        public Guid EmployeeId { get; set; }
        public SocialSecurityNumber SocialSecurityNumber { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set; }
    }
}