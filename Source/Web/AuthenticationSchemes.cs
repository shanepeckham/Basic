using System.Collections.Generic;
using System.Linq;
using doLittle.Read;

namespace Web
{
    public class AuthenticationSchemes : IQueryFor<AuthenticationScheme>
    {
        public IQueryable<AuthenticationScheme> Query
        {
            get 
            {
                var schemes = HttpContextBinding.Current.Authentication.GetAuthenticationSchemes();
                return schemes.Where(_ => _.DisplayName != null).Select(_ => new AuthenticationScheme
                {
                    Scheme = _.AuthenticationScheme,
                    DisplayName = _.DisplayName
                }).AsQueryable();
            }
        }
    }
}