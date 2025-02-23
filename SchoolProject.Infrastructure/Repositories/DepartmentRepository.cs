
namespace SchoolProject.Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepositoryAsync<Department>, IDepartmentRepository
    {
        #region Filds
        private readonly DbSet<Department> _departmentRepository;

        #endregion

        #region Constructors
        public DepartmentRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _departmentRepository = dbContext.Set<Department>();
        }

        #endregion

        #region Handles Methods
        public Task<List<Department>> GetDepartmentsAsync()
        {
            return _departmentRepository.ToListAsync();
        }
        #endregion
    }
}
