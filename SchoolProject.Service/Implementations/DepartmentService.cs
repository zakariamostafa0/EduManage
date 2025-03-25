
using SchoolProject.Data.Entities.Procedure;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrastructure.Abstracts.Procedure;
using SchoolProject.Infrastructure.Abstracts.Views;

namespace SchoolProject.Service.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IViewRepository<ViewDepartment> _viewDepartmentRepository;
        private readonly IProcedureRepository _procedureRepository;
        #endregion

        #region Constructors
        public DepartmentService
         (
            IDepartmentRepository departmentRepository,
            IViewRepository<ViewDepartment> viewDepartmentRepository,
            IProcedureRepository procedureRepository)
        {
            _departmentRepository = departmentRepository;
            _viewDepartmentRepository = viewDepartmentRepository;
            _procedureRepository = procedureRepository;
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
        public async Task<IReadOnlyList<DepartmentStudentsCountProc>> GetDepartmentStudentsCountProc(DepartmentStudentsCountProcParmeters parmeters)
        {
            return await _procedureRepository.GetDepartmentStudentsCountProc(parmeters);
        }
        #endregion
    }
}
