using System;
using System.Security.Claims;
using AdCampaign.DAL.Entities;

namespace AdCampaign.Authetication
{
    public static class ClaimExt
    {
        public static long GetId(this ClaimsPrincipal principal)
        {
            AssertAuthentication(principal);

            return long.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        public static string GetLogin(this ClaimsPrincipal principal)
        {
            AssertAuthentication(principal);

            return principal.FindFirst(ClaimTypes.Name)!.Value;
        }

        public static string GetEmail(this ClaimsPrincipal principal)
        {
            AssertAuthentication(principal);

            return principal.FindFirst(ClaimTypes.Email)!.Value;
        }

        public static Role GetRole(this ClaimsPrincipal principal)
        {
            AssertAuthentication(principal);

            var role = principal.FindFirst(ClaimTypes.Role)!.Value;
            return Enum.Parse<Role>(role);
        }

        public static bool IsAdministrator(this ClaimsPrincipal principal)
        {
            AssertAuthentication(principal);

            var role = GetRole(principal);
            return role == Role.Administrator;
        }

        public static bool IsModerator(this ClaimsPrincipal principal)
        {
            AssertAuthentication(principal);

            var role = GetRole(principal);
            return role == Role.Moderator;
        }

        public static bool IsAdvertiser(this ClaimsPrincipal principal)
        {
            AssertAuthentication(principal);

            var role = GetRole(principal);
            return role == Role.Advertiser;
        }
        
        public static bool IsAdministratorOrModerator(this ClaimsPrincipal principal)
        {
            AssertAuthentication(principal);

            var role = GetRole(principal);
            return role == Role.Moderator || role == Role.Administrator;
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