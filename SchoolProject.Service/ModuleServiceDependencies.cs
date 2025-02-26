using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Implementations;
using System.Collections.Concurrent;

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
            services.AddSingleton(new ConcurrentDictionary<string, RefreshToken>());
            return services;
        }
    }
}
