namespace SchoolProject.Infrastructure.Abstracts
{
    public interface IDepartmentRepository : IGenericRepositoryAsync<Department>
    {
        public Task<List<Department>> GetDepartmentsAsync();
    }
}
