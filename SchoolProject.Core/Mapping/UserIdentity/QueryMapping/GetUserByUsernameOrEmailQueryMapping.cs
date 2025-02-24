using SchoolProject.Core.Features.UserIdentity.Queries.Results;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.UserIdentity
{
    public partial class UserIdentityProfile
    {
        public void GetUserByUsernameOrEmailQueryMapping()
        {
            CreateMap<ApplicationUser, GetUserByUsernameOrEmailResponse>();
        }
    }
}
