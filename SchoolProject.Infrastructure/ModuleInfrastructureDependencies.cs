using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrastructure.Abstracts.Procedure;
using SchoolProject.Infrastructure.Abstracts.Views;
using SchoolProject.Infrastructure.Repositories;
using SchoolProject.Infrastructure.Repositories.Procedure;
using SchoolProject.Infrastructure.Repositories.Views;

namespace SchoolProject.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));

            //View 
            services.AddTransient<IViewRepository<ViewDepartment>, ViewDepartmentRepository>();

            //Procedures 
            services.AddTransient<IProcedureRepository, ProcedureRepository>();

            return services;
        }
    }
}
