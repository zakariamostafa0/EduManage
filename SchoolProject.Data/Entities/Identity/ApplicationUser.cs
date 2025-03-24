using Microsoft.AspNetCore.Identity;

namespace SchoolProject.Data.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            UserRefreshTokens = new HashSet<UserRefreshToken>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        //public string? Country { get; set; }
        public string? Code { get; set; }

        [InverseProperty(nameof(UserRefreshToken.User))]
        public ICollection<UserRefreshToken> UserRefreshTokens { get; set; }

    }
}
