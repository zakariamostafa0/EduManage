namespace SchoolProject.Service.Abstracts
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsListAsync();
        public IQueryable<Student> GetFilterStudentPaginatedQuerable(StudentOrderingEnum OrderingEnum, string? search);
        public Task<Student> GetStudentByIdAsync(int id);
        public Task<Student> AddStudentAsync(Student student);
        public Task<bool> EditStudentAsync(Student student);
        public Task<bool> DeleteStudentAsync(int id);
        public Task<bool> IsNameExist(string name, int? id = null);


    }
}
