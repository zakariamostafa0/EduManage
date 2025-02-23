
namespace SchoolProject.Service.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields
        private readonly IDepartmentRepository _departmentRepository;

        #endregion

        #region Constructors
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
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
        #endregion
    }
}
