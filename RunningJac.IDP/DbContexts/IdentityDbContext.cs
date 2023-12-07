using Microsoft.EntityFrameworkCore;
using RunningJac.IDP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RunningJac.IDP.DbContexts
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasData
                (
                    new User()
                    { 
                        Id = "dab7b0a2-54a9-4d32-94e4-9a7f8d26ecdf",
                        Subject = "e7cd9d8e-38e9-4b6a-a7b0-8c17b4cf10d1",
                        Username = "Odira",
                        IsActive = true,
                        Email = "odi@gmail.com",
                        Password = "#&okodf1t90ejyuhDF"
                    },
                    new User()
                    { 
                        Id = "8e48e3d7-11f9-4ecb-9cd5-9514fe4a09b6",
                        Subject = "f9f95db5-82b3-4aa6-84b7-35c586be8042",
                        IsActive = true,
                        Username = "Somto",
                        Email = "sommy@gmail.com",
                        Password = "#%6wr90ujkjwmklOLw"
                    }
                );

            builder.Entity<UserClaim>().HasData
                (
                    new UserClaim()
                    {
                        Id = "b37d1681-6d0c-4cb5-8d98-0a446f9765e1",
                        UserId = "dab7b0a2-54a9-4d32-94e4-9a7f8d26ecdf",
                        Type = "given_name",
                        Value = "Odira"
                    },
                    new UserClaim()
                    {
                        Id = "c03d7464-2cf2-4f3c-81b0-3f7da739e383",
                        UserId = "dab7b0a2-54a9-4d32-94e4-9a7f8d26ecdf",
                        Type = "family_name",
                        Value = "Ike"
                    },
                    new UserClaim()
                    {
                        Id = "a0f1b4f0-3809-4c4f-a3f0-17787d2c013c",
                        UserId = "dab7b0a2-54a9-4d32-94e4-9a7f8d26ecdf",
                        Type = "role",
                        Value = "admin"
                    },
                    new UserClaim()
                    {
                        Id = "9a0bfa87-df71-4b07-a3c3-9ec61b7ec2a5",
                        UserId = "dab7b0a2-54a9-4d32-94e4-9a7f8d26ecdf",
                        Type = "country",
                        Value = "ng"
                    },
                    new UserClaim()
                    {
                        Id = "c8c66c9f-b309-417b-9b4b-785c02f6f3d8",
                        UserId = "8e48e3d7-11f9-4ecb-9cd5-9514fe4a09b6",
                        Type = "given_name",
                        Value = "Somto"
                    },
                    new UserClaim()
                    {
                        Id = "4bdfec12-318a-4df6-a1a7-94272905724e",
                        UserId = "8e48e3d7-11f9-4ecb-9cd5-9514fe4a09b6",
                        Type = "family_name",
                        Value = "Ikewelugo"
                    },
                    new UserClaim()
                    {
                        Id = "4ef412d3-8e06-4ab3-b2a1-7e2246824cd9",
                        UserId = "8e48e3d7-11f9-4ecb-9cd5-9514fe4a09b6",
                        Type = "role",
                        Value = "regular_user"
                    },
                    new UserClaim()
                    {
                        Id = "946bc5b4-0a3a-4f47-946d-94a149e24256",
                        UserId = "8e48e3d7-11f9-4ecb-9cd5-9514fe4a09b6",
                        Type = "country",
                        Value = "ng"
                    }
                );
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var updatedConcurrencyAwareEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified)
                .OfType<BaseEntity>();

            foreach (var entry in updatedConcurrencyAwareEntries)
            {
                entry.ConcurrencyStamp = Guid.NewGuid().ToString();
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
