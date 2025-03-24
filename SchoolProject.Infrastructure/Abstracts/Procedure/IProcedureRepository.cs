using SchoolProject.Data.Entities.Procedure;

namespace SchoolProject.Infrastructure.Abstracts.Procedure
{
    public interface IProcedureRepository
    {
        public Task<IReadOnlyList<DepartmentStudentsCountProc>> GetDepartmentStudentsCountProc(DepartmentStudentsCountProcParmeters parmeters);
    }
}
