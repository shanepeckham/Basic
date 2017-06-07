using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(sharedOptions =>
                sharedOptions.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);

            services.AddRouting();
        }

        AsyncLocal<ClaimsPrincipal> _currentPrincipal = new AsyncLocal<ClaimsPrincipal>();
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Microsoft.Extensions.Logging.LogLevel.Trace);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            HttpContextBinding.Configure(app);

            //app.UseSinglePageApplication(env);
            app.UseDefaultFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                    context.Context.Response.Headers.Add("Expires", "-1");
                }
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
                //LoginPath = new PathString("/signin"),
                //LogoutPath = new PathString("/signout")
            });


            var clientId = "basic";
            var clientSecret = "secret";
            var authority = "http://localhost:5001";

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                SignInScheme = "Cookies",

                ClientId = clientId,
                Authority = authority,
                GetClaimsFromUserInfoEndpoint = true,
                
                //AuthenticationScheme = "oidc",
                //ClientSecret = clientSecret,
                //ResponseType = OpenIdConnectResponseType.Code,
                RequireHttpsMetadata = false,
                SaveTokens = true
            });

            /*
            if( ClaimsPrincipal.ClaimsPrincipalSelector == null )
            {
                app.Use(async (context, next) =>
                {
                    _currentPrincipal.Value = context.User;
                    await next();
                });
                ClaimsPrincipal.ClaimsPrincipalSelector = () => _currentPrincipal.Value;
            }*/
            

            app.UsedoLittle(env);

            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapRoute("Authentication/Challenge", async context =>
            {
                if (!context.User.Identities.Any(identity => identity.IsAuthenticated))
                {
                    await HttpContextBinding.Current.Authentication.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties
                    {
                        RedirectUri = "/"
                    });
                }
                else
                {
                    context.Response.Redirect("/");
                }
            });

            routeBuilder.MapRoute("Authentication/Identity", context => {
                
                return Task.CompletedTask;
            });

            routeBuilder.MapRoute("Authentication/SignOut", async context =>
            {
                await context.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                context.Response.Redirect("/");
            });

            var routes = routeBuilder.Build();
            app.UseRouter(routes);
        }
    }
}


// https://github.com/aspnet/Security/tree/dev/samples/OpenIdConnect.AzureAdSample
// Old: https://github.com/Azure-Samples/active-directory-dotnet-webapp-openidconnect-aspnetcore/blob/master/WebApp-OpenIDConnect-DotNet

// https://stormpath.com/blog/token-authentication-asp-net-core
// https://andrewlock.net/a-look-behind-the-jwt-bearer-authentication-middleware-in-asp-net-core/
// https://blogs.msdn.microsoft.com/webdev/2017/04/06/jwt-validation-and-authorization-in-asp-net-core/
// https://blogs.msdn.microsoft.com/webdev/2016/10/27/bearer-token-authentication-in-asp-net-core/
// http://www.jerriepelser.com/blog/aspnetcore-jwt-saving-bearer-token-as-claim/
// https://jonhilton.net/2017/05/03/login-authentication-asp-net-core-web-api-big-picture/
// https://dev.to/samueleresca/developing-token-authentication-using-aspnet-core

// https://damienbod.com/2017/05/06/secure-asp-net-core-mvc-with-angular-using-identityserver4-openid-connect-hybrid-flow/
// https://github.com/damienbod/AspNet5IdentityServerAngularImplicitFlow

// https://www.itunity.com/article/angular-2-openid-connect-azure-active-directory-3093

// https://channel9.msdn.com/Blogs/Seth-Juarez/Advanced-aspNET-Core-Authorization-with-Barry-Dorrans

// Issues and fixes:
// https://github.com/aspnet/Security/issues/1068

// https://identityserver4.readthedocs.io/en/release/quickstarts/7_javascript_client.html


/*
app.Run(async context =>
{
    if (context.Request.Path.Equals("/signout"))
    {
        await context.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync($"<html><body>Signing out {context.User.Identity.Name}<br>{Environment.NewLine}");
        await context.Response.WriteAsync("<a href=\"/\">Sign In</a>");
        await context.Response.WriteAsync($"</body></html>");
        return;
    }

    if (!context.User.Identities.Any(identity => identity.IsAuthenticated))
    {
        await context.Authentication.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "/" });
        return;
    }


    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync($"<html><body>Hello Authenticated User {context.User.Identity.Name}<br>{Environment.NewLine}");
    await context.Response.WriteAsync("Claims:<br>" + Environment.NewLine);
    foreach (var claim in context.User.Claims)
    {
        await context.Response.WriteAsync($"{claim.Type}: {claim.Value}<br>{Environment.NewLine}");
    }

    await context.Response.WriteAsync("Tokens:<br>" + Environment.NewLine);
    try
    {
        // Use ADAL to get the right token
        var authContext = new AuthenticationContext(authority, AuthPropertiesTokenCache.ForApiCalls(context, CookieAuthenticationDefaults.AuthenticationScheme));
        var credential = new ClientCredential(clientId, clientSecret);
        string userObjectID = context.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
        var result = await authContext.AcquireTokenSilentAsync(resource, credential, new UserIdentifier(userObjectID, UserIdentifierType.UniqueId));

        await context.Response.WriteAsync($"access_token: {result.AccessToken}<br>{Environment.NewLine}");
    }
    catch (Exception ex)
    {
        await context.Response.WriteAsync($"AquireToken error: {ex.Message}<br>{Environment.NewLine}");
    }

    await context.Response.WriteAsync("<a href=\"/signout\">Sign Out</a>");
    await context.Response.WriteAsync($"</body></html>");

});            
*/


/*
var resource = "https://graph.windows.net";
Events = new OpenIdConnectEvents()
{
OnAuthorizationCodeReceived = async context =>
{
    var schemes = context.HttpContext.Authentication.GetAuthenticationSchemes();

    var request = context.HttpContext.Request;
    var currentUri = UriHelper.BuildAbsolute(request.Scheme, request.Host, request.PathBase, request.Path);
    var credential = new ClientCredential(clientId, clientSecret);
    var authContext = new AuthenticationContext(authority, AuthPropertiesTokenCache.ForCodeRedemption(context.Properties));

    var result = await authContext.AcquireTokenByAuthorizationCodeAsync(
        context.ProtocolMessage.Code, new Uri(currentUri), credential, resource);

    context.HandleCodeRedemption();
    context.HandleResponse();
},
OnAuthenticationFailed = context =>
{
    context.HandleResponse();
    return Task.CompletedTask;
}
}*/

            /*
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(clientSecret));
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                Audience = "http://localhost:5000",
                Authority = authority,
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                RequireHttpsMetadata = false,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = true,
                    ValidIssuer = "",
                    ValidateAudience = true,
                    ValidAudience = "",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }
            });
            */
