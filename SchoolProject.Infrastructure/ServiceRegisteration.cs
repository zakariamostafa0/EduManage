using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection AddServiceRegisteration(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                // Password settings.
                option.Password.RequireDigit = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.Password.RequiredLength = 6;
                option.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(.5);
                option.Lockout.MaxFailedAccessAttempts = 10;
                option.Lockout.AllowedForNewUsers = true;

                // User settings.
                option.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                option.User.RequireUniqueEmail = true;
                option.SignIn.RequireConfirmedEmail = false;

            }).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();
            return services;
        }
    }
}
