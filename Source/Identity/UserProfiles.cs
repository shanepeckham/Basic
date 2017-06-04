using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Extensions;
using Microsoft.Extensions.Logging;

namespace Identity
{
    public class UserProfiles : IProfileService
    {
        /// <summary>
        /// The logger
        /// </summary>
        readonly ILogger Logger;

        /// <summary>
        /// The users
        /// </summary>
        readonly Users Users;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestUserProfileService"/> class.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <param name="logger">The logger.</param>
        public UserProfiles(Users users, ILogger<UserProfiles> logger)
        {
            Users = users;
            Logger = logger;
        }

        /// <summary>
        /// This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint)
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            if (context.RequestedClaimTypes.Any())
            {
                var user = Users.FindBySubjectId(context.Subject.GetSubjectId());
                if (user != null)
                {
                    if (context.RequestedClaimTypes.Any())
                        context.IssuedClaims.AddRange(context.FilterClaims(user.Claims));
                }
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// This method gets called whenever identity server needs to determine if the user is valid or active (e.g. if the user's account has been deactivated since they logged in).
        /// (e.g. during token issuance or validation).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task IsActiveAsync(IsActiveContext context)
        {
            var user = Users.FindBySubjectId(context.Subject.GetSubjectId());
            context.IsActive = user != null;

            return Task.FromResult(0);
        }
    }
}