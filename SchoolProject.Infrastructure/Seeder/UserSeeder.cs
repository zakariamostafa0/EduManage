using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> _userManager)
        {
            var userCount = await _userManager.Users.CountAsync();
            if (userCount <= 0)
            {
                var defaultUser = new ApplicationUser
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Address = "Cairo",
                    Email = "admin@admin.com",
                    UserName = "admin",
                    //Country = "Egypt",
                    PhoneNumber = "1000000000",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await _userManager.CreateAsync(defaultUser, "Adm!n147951");
                await _userManager.AddToRoleAsync(defaultUser, "Admin");
            }
        }
    }
}
