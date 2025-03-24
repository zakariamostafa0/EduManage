using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.AuthService.Implementaions;
using SchoolProject.Service.AuthService.Interfaces;
using SchoolProject.Service.Implementations;

namespace SchoolProject.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            // services.AddSingleton(new ConcurrentDictionary<string, RefreshToken>());
            return services;
        }
    }
}
