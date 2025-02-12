using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class StudentService : IStudentService
    {
        #region Fields
        private readonly IStudentRepository _studentRepository;
        #endregion

        #region Constructors
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        #endregion

        #region Handles Methods
        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _studentRepository.GetStudentsAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var student = _studentRepository.GetTableNoTracking()
                .Include(s => s.Department)
                .Where(s => s.StudID == id)
                .FirstOrDefault();
            return student;
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            var studentResult = await _studentRepository.AddAsync(student);
            return studentResult;
        }

        public async Task<bool> IsNameExist(string name)
        {
            var studentResult = _studentRepository.GetTableNoTracking()
                .Where(s => s.Name == name).FirstOrDefault();
            if (studentResult == null) //if he found name return TRUE
                return false;
            return true;
        }

        #endregion
    }
}
