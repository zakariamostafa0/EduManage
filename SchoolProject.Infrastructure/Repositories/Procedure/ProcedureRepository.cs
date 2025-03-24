using SchoolProject.Data.Entities.Procedure;
using SchoolProject.Infrastructure.Abstracts.Procedure;
using StoredProcedureEFCore;

namespace SchoolProject.Infrastructure.Repositories.Procedure
{
    public class ProcedureRepository : IProcedureRepository
    {
        #region Filds
        private readonly ApplicationDBContext _dbContext;
        #endregion

        #region Constructors
        public ProcedureRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Handles Methods
        public async Task<IReadOnlyList<DepartmentStudentsCountProc>> GetDepartmentStudentsCountProc(DepartmentStudentsCountProcParmeters parmeters)
        {
            var rows = new List<DepartmentStudentsCountProc>();
            await _dbContext.LoadStoredProc(nameof(DepartmentStudentsCountProc))
                .AddParam(nameof(DepartmentStudentsCountProcParmeters.Did), parmeters.Did)
                .ExecAsync(async r => rows = await r.ToListAsync<DepartmentStudentsCountProc>());
            return rows;
        }
        #endregion
    }
}
