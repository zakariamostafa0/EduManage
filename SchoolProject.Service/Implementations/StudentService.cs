using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<string> AddStudentAsync(Student student)
        {
            await _studentRepository.AddAsync(student);
            return "Success";
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
