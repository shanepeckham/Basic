using doLittle.Security;
using doLittle.Read;
using System;
using doLittle.Configuration;
using System.Security.Claims;

namespace Read.HumanResources.Employees
{
    public class MyRule : ISecurityRule
    {
        public string Description => "Custom rule";

        public bool IsAuthorized(object securable)
        {
            var principalResolver = Configure.Instance.Container.Get<ICanResolvePrincipal>();
            var principal = principalResolver.Resolve();
            var currentPrincipal = ClaimsPrincipal.Current;

            var roles = currentPrincipal.FindAll("role");
           
            return true;
        }
    }

    public class SecurityDescriptor : BaseSecurityDescriptor
    {
        public SecurityDescriptor()
        {
            When.Fetching().ReadModels().InNamespace(typeof(Employees).Namespace, _ => {
                _.User().AddRule(new MyRule());
                //_.User().MustHaveClaimType("name");
            });
        }
    }
}