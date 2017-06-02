using System;
using doLittle.Commands;

namespace Domain.HumanResources.Employees
{
    public class Register : ICommand
    {
        public Guid EmployeeId { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set; }
    }
}