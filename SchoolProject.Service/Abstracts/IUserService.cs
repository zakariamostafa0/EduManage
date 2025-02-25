using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.Abstracts
{
    public interface IUserService
    {
        UserManager<ApplicationUser> UserManager { get; }
        public Task<bool> IsUserNameExist(string name, string? id = null);
        public Task<bool> IsEmailExist(string name, string? id = null);


    }
}
