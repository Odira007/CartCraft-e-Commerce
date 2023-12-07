using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RunningJac.IDP.Quickstart.Registration
{
    public class RegistrationInputModel
    {
        [Required]
        [MaxLength(50)]
        [ConcurrencyCheck]
        public string Firstname { get; set; }
        [Required]
        [MaxLength(50)]
        [ConcurrencyCheck]
        public string Lastname { get; set; }
        [Required]
        [MaxLength(50)]
        [ConcurrencyCheck]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[\W_]).{6,20}$")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        //[Required]
        //public string Country { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string ReturnUrl { get; set; }
    }
}
