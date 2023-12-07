using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningJac.IDP.Entities
{
    public class User : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Subject { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        //public string Country { get; set; }
        public string Email { get; set; }
        public string SecurityCode { get; set; }
        public DateTime SecurityCodeExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public ICollection<UserClaim> Claims { get; set; } = new List<UserClaim>();
    }
}
