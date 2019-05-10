using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using utcAltkomDevices.IServices;
using utcAltkomDevices.Models;

namespace utcAltkomDevices.WebService.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private IUsersService userService;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IUsersService userService) : base(options, logger, encoder, clock)
        {
            this.userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing authorization header");
            }
            else
            {
                User userObj = ParseAuthentication(Request.Headers["Authorization"]);

                if (userObj != null)
                {
                    // We can add more info to the Claim, to be injected into authenticated User 
                    Claim[] claims = new[]
                    {
                        new Claim("Hey","Hola"),
                        new Claim(ClaimTypes.Name,userObj.Name),
                        new Claim(ClaimTypes.NameIdentifier, userObj.Id.ToString()),
                        //We can also add roles to check for permisions in controllers
                        new Claim(ClaimTypes.Role, userObj.Role),
                        new Claim(ClaimTypes.Role, "UserRole")
                    };

                    IIdentity identity = new ClaimsIdentity(claims, Scheme.Name);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    

                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
                else
                {
                    return AuthenticateResult.Fail("Wrong pass or user");
                }

            }
        }

        //TODO: Is there any prettier way to do this in .NetCore?
        private User ParseAuthentication(string authorizationHeader)
        {
            var authHeader = AuthenticationHeaderValue.Parse(authorizationHeader);
            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');

            return userService.Authenticate(credentials[0], credentials[1]);
        }
    }
}
