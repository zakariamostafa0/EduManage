namespace SchoolProject.Core.Mapping.UserIdentity
{
    public partial class UserIdentityProfile : Profile
    {
        public UserIdentityProfile()
        {
            AddUserCommandMapping();
            UpdateUserCommandMapping();
            GetUsersQueryMapping();
            GetUserByUsernameOrEmailQueryMapping();
        }
    }
}
