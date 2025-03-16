using SchoolProject.Core.Features.Authorization.Queries.Results;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.RoleIdentity
{
    public partial class RoleIdentityProfile
    {
        public void GetAllRolesQueryMap()
        {
            CreateMap<ApplicationRole, GetRoleResult>();
        }
    }
}
