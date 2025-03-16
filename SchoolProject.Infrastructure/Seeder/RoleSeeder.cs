using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Enums;

namespace SchoolProject.Infrastructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<ApplicationRole> _roleManager)
        {
            var roles = Enum.GetNames(typeof(UserRoles)); // Convert enum to list of strings
            var roleCount = await _roleManager.Roles.CountAsync();
            if (roleCount <= 0)
            {
                foreach (var roleName in roles)
                {
                    //if (!await _roleManager.RoleExistsAsync(roleName)) // Check if role exists
                    await _roleManager.CreateAsync(new ApplicationRole { Name = roleName });
                }
            }
        }
    }
}
