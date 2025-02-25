using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.Implementations
{
    public class UserService : IUserService
    {
        #region Fields
        public UserManager<ApplicationUser> UserManager { get; }


        #endregion

        #region Constructors
        public UserService(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        #endregion

        #region Handles Methods
        public async Task<bool> IsEmailExist(string email, string? id = null)
        {
            if (id is not null)
            {
                var userResult = UserManager.Users.AsQueryable()
                .Where(s => s.Email == email & !s.Id.Equals(id)).FirstOrDefault();
                if (userResult == null)
                    return false;
                return true;
            }
            else
            {
                var userResult = UserManager.Users.AsQueryable()
                .Where(s => s.Email == email).FirstOrDefault();
                if (userResult == null)
                    return false;
                return true;
            }
        }

        public async Task<bool> IsUserNameExist(string username, string? id = null)
        {
            if (id is not null)
            {
                var userResult = UserManager.Users.AsQueryable()
                .Where(s => s.UserName == username & !s.Id.Equals(id)).FirstOrDefault();
                if (userResult == null)
                    return false;
                return true;
            }
            else
            {
                var userResult = UserManager.Users.AsQueryable()
                .Where(s => s.UserName == username).FirstOrDefault();
                if (userResult == null)
                    return false;
                return true;
            }
        }
        #endregion

    }
}
