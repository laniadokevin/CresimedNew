
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Cresimed.Core.Entities.Database;

namespace Cresimed.Campus
{
    public class SecurityManager
    {
        public async void SignIn(HttpContext httpContext, User user)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(getUserClaims(user), CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        }

        public async void SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }

        private IEnumerable<Claim> getUserClaims(User user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()));
            user.UserRoles.ToList().ForEach(ac =>
            {
                if (ac.Enable)
                    claims.Add(new Claim(ClaimTypes.Role, ac.Role.Name));
            });
            return claims;
        }
    }
}