namespace SchoolProject.Infrastructure.Repositories
{
    public class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
    {
        #region Fields
        private readonly DbSet<Student> _studentRepository;
        #endregion

        #region Constructors
        public StudentRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _studentRepository = dbContext.Set<Student>();
        }
        #endregion

        #region Handles Methods
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _studentRepository.Include(s => s.Department).ToListAsync();
        }
        #endregion


    }
}
