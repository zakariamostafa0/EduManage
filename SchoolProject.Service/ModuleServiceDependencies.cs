using Microsoft.Extensions.DependencyInjection;
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
            return services;
        }
    }
}
