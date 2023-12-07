using Microsoft.AspNetCore.Authorization;
using System;

namespace CartCraft.Authorization
{
    public class AuthorizationPolicies
    {
        public static AuthorizationPolicy CanAddItem()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole("admin", "moderator")
                .RequireClaim("country", "ng")
                .Build();
        }
    }
}
