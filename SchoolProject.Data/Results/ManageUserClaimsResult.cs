using System.Text.Json.Serialization;

namespace SchoolProject.Data.Results
{
    public class ManageUserClaimsResult
    {
        public string UserId { get; set; }
        [JsonIgnore]
        public bool Success { get; set; }
        public List<UserClaims> UserClaims { get; set; }
    }

    public class UserClaims
    {
        public string Type { get; set; }
        public bool Value { get; set; }
    }
}
