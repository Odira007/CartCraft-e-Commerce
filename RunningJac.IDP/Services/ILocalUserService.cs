using RunningJac.IDP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RunningJac.IDP.Services
{
    public interface ILocalUserService
    {
        Task<User> GetUserByAsync(Expression<Func<User, bool>> filter, List<string> includes = null);
        Task<List<UserClaim>> GetClaimsByAsync(string subject);
        // Task CreateClaimsByUserSubject(string subject, List<UserClaim> claims);
        Task CreateUserAsync(User user, string password);
        Task<bool> ValidateCredentialsAsync(string username, string password);
        Task<bool> IsUserActive(string subject);
        Task<bool> ActivateUserAsync(string securityCode);
        Task<bool> SaveChangesAsync();

    }
}
