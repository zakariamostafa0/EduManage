using System.Security.Claims;

namespace SchoolProject.Data.Helpers
{
    public static class ClaimStore
    {
        public static List<Claim> claims = new List<Claim>()
        {
            new Claim("Create Student","false"),
            new Claim("Update Student","false"),
            new Claim("Delete Student","false")
        };
    }
}
