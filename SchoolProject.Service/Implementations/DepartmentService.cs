
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrastructure.Views;

namespace SchoolProject.Service.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IViewRepository<ViewDepartment> _viewDepartmentRepository;

        #endregion

        #region Constructors
        public DepartmentService
         (
            IDepartmentRepository departmentRepository,
            IViewRepository<ViewDepartment> viewDepartmentRepository
         )
        {
            _departmentRepository = departmentRepository;
            _viewDepartmentRepository = viewDepartmentRepository;
        }


        #endregion

        #region Handles Methods
        public async Task<Department> GetDepartmentById(int id)
        {
            var department = await _departmentRepository.GetTableNoTracking().Where(x => x.DID == id)
                                                .Include(x => x.Instructors)
                                                .Include(x => x.Instructor)
                                                .Include(x => x.DeptSubjects).ThenInclude(ds => ds.Subject).FirstOrDefaultAsync();
            return department;
        }

        public async Task<List<ViewDepartment>> GetViewDepartments()
        {
            var viewDepartment = await _viewDepartmentRepository.GetTableNoTracking().ToListAsync();
            return viewDepartment;
        }

        public async Task<bool> IsDepartmentExist(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
                return false;
            return true;
        }
        #endregion
    }
}
