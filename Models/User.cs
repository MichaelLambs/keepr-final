using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace keepr_final.Models
{
    public class User
    {
        [Key] // PUT BOTH HERE AND PRIMARY KEY IN SQL
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(4)]
        public string Password { get; set; }
    }

    public class UserCreateModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(4)]
        public string Password { get; set; }
    }

    // LOGGED IN USER DETAILS. !!!DOES NOT RETURN PASSWORD. BIGGEST IMPORTANCE!!!
    public class UserReturnModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }

        // FOR AUTO LOGIN
        public ClaimsPrincipal SetClaims()
        {
            // BELOW SAME AS "req.session.uid = user._id => req.session.save"
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Email, Email), // SAVES USER HASHED EMAIL ADDRESS INTO USER COOKIE "REQUEST HEADER"
                new Claim(ClaimTypes.Name, Id) // SAVES USER HASHED ID INTO USER COOKIE "REQUEST HEADER" 
            };
            // HOW MICROSOFT HANDELS SECURITY CLAIMS
            var userIdentity = new ClaimsIdentity(claims, "login");
            var principal = new ClaimsPrincipal(userIdentity);
            return principal;
        }
    }

    // USER NOT LOGGED IN
    public class PublicUserModel
    {
        public string Name { get; set; }
    }

    // WHAT IS NEEDED TO LOGIN
    public class UserLoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    // // WHAT IS NEEDED TO CHANGE PASSWORD
    // public class ChangeUserPasswordModel
    // {
    //     public int Id { get; set; }
    //     [MaxLength(255), EmailAddress]
    //     public string Email { get; set; }
    //     [Required, MinLength(4)]
    //     public string OldPassword { get; set; }
    //     [Required, MinLength(4)]
    //     public string NewPassword { get; set; }
    // }
}