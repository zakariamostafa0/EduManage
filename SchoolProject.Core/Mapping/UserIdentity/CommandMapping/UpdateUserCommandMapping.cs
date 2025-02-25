using SchoolProject.Core.Features.UserIdentity.Commands.Models;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.UserIdentity
{
    public partial class UserIdentityProfile
    {
        public void UpdateUserCommandMapping()
        {
            CreateMap<UpdateUserCommand, ApplicationUser>();
        }
    }
}
