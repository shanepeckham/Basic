using doLittle.Concepts;

namespace Concepts.HumanResources.Employees
{
    public class SocialSecurityNumber : ConceptAs<string>
    {
        public static implicit operator SocialSecurityNumber(string ssn)
        {
            return new SocialSecurityNumber { Value = ssn };
        }
    }
}