using SchoolProject.Data.Entities.Views;

namespace SchoolProject.Service.Abstracts
{
    public interface IDepartmentService
    {
        public Task<Department> GetDepartmentById(int id);
        public Task<bool> IsDepartmentExist(int id);
        public Task<List<ViewDepartment>> GetViewDepartments();
    }
}
