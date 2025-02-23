namespace SchoolProject.Infrastructure.Abstracts
{
    public interface IStudentRepository : IGenericRepositoryAsync<Student>
    {
        public Task<List<Student>> GetStudentsAsync();
    }
}
