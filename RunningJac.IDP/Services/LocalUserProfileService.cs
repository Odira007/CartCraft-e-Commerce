using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using RunningJac.IDP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RunningJac.IDP.Services
{
    public class LocalUserProfileService : IProfileService
    {
        private readonly ILocalUserService _localUserService;

        public LocalUserProfileService(ILocalUserService localUserService)
        {
            _localUserService = localUserService;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subject = context.Subject.GetSubjectId();
            var claims = await _localUserService.GetClaimsByAsync(subject);

            context.AddRequestedClaims(claims
                .Select(x => new Claim(x.Type, x.Value)).ToList());
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subject = context.Subject.GetSubjectId();
            context.IsActive = await _localUserService.IsUserActive(subject);
        }
    }
}
