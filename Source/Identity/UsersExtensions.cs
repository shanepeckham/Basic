using Microsoft.Extensions.DependencyInjection;

namespace Identity
{
    public static class UsersExtensions
    {

        public static IIdentityServerBuilder AddUserStore(this IIdentityServerBuilder builder)
        {
            builder.Services.AddSingleton(new Users());
            builder.AddProfileService<UserProfiles>();
            builder.AddResourceOwnerValidator<UserResourceOwnerPasswordValidator>();
            return builder;
        }
    }
}