using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RunningJac.IDP.DbContexts;
using RunningJac.IDP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace RunningJac.IDP.Services
{
    public class LocalUserService : ILocalUserService
    {
        private readonly IdentityDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public LocalUserService(IdentityDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> ActivateUserAsync(string securityCode)
        {
            if (string.IsNullOrEmpty(securityCode)) throw new ArgumentNullException(nameof(securityCode));

            var user = await _context.Users.FirstOrDefaultAsync
                (x => x.SecurityCode == securityCode && x.SecurityCodeExpirationDate >= DateTime.UtcNow);

            if (user == null) return false;

            user.IsActive = true;
            user.SecurityCode = null;

            return true;
        }

        public async Task CreateUserAsync(User user, string password)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            if (await _context.Users.AnyAsync(x => x.Username == user.Username))
                throw new InvalidOperationException("Username must be unique");

            // if (await _context.Users.AnyAsync(x => x.Email == user.Email))
                // throw new InvalidOperationException("Email must be unique");


            user.Password = _passwordHasher.HashPassword(user, password);
            var rng = RandomNumberGenerator.Create();
            byte[] data = new byte[128];
            rng.GetBytes(data);

            user.SecurityCode = Convert.ToBase64String(data);
            user.SecurityCodeExpirationDate = DateTime.UtcNow.AddMinutes(30);

            await _context.Users.AddAsync(user);
        }

        //public async Task CreateClaimsByUserSubject(string subject, List<UserClaim> claims)
        //{
        //    if (string.IsNullOrEmpty(subject)) throw new ArgumentNullException(nameof(subject));

        //    var user = await GetUserByAsync(x => x.Subject == subject);
        //    if (user == null) throw new Exception("User was null");

        //    foreach (var claim in claims)
        //    {
        //        claim.Id = Guid.NewGuid().ToString();
        //        user.Claims.Add(claim);
        //    }
        //}

        public async Task<User> GetUserByAsync(Expression<Func<User, bool>> filter, List<string> includes = null)
        {
            if (includes != null)
            {
                foreach(var include in includes)
                    return await _context.Users.Include(include).Where(filter).FirstOrDefaultAsync();
            }

            return await _context.Users.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<List<UserClaim>> GetClaimsByAsync(string subject)
        {
            if (string.IsNullOrEmpty(subject)) throw new ArgumentNullException(nameof(subject));
            
            return await _context.UserClaims.Where(x => x.User.Subject == subject).ToListAsync();
        }

        public async Task<bool> IsUserActive(string subject)
        {
            if (string.IsNullOrEmpty(subject)) return false;

            var user = await GetUserByAsync(x => x.Subject == subject);
            return user.IsActive;
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return false;

            var user = await GetUserByAsync(x => x.Username == username);

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, _passwordHasher
                .HashPassword(user, user.Password), user.Password);
            return verificationResult == PasswordVerificationResult.Success;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
