using System.Text.Json.Serialization;

namespace SchoolProject.Data.Results
{
    public class ManageUserRolesResult
    {
        public string UserId { get; set; }
        [JsonIgnore]
        public bool Success { get; set; }
        public List<Roles> Roles { get; set; }
    }

    public class Roles
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool HasRole { get; set; }
    }
}
