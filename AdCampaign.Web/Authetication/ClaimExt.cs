using System;
using System.Security.Claims;
using AdCampaign.DAL.Entities;

namespace AdCampaign.Authetication
{
    public static class ClaimExt
    {
        public static string GetLogin(this ClaimsPrincipal principal)
        {
            AssertAuthentication(principal);

            return principal.FindFirst(ClaimTypes.Name)!.Value;
        }

        public static Role GetRole(this ClaimsPrincipal principal)
        {
            AssertAuthentication(principal);

            var role = principal.FindFirst(ClaimTypes.Role)!.Value;
            return Enum.Parse<Role>(role);
        }

        private static void AssertAuthentication(ClaimsPrincipal principal)
        {
            if (!principal.Identity!.IsAuthenticated)
            {
                throw new InvalidOperationException("Unknown authentication");
            }
        }
    }
}