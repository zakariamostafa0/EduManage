using Microsoft.AspNetCore.Http;

namespace SchoolProject.Service.Abstracts
{
    public interface IInstructorService
    {
        public Task<decimal> GetSalarySummationOfInstructor();
        public Task<bool> IsNameExist(string nameEn);
        public Task<bool> IsNameExistExcludeSelf(string nameEn, int id);
        public Task<string> AddInstructorAsync(Instructor instructor, IFormFile file);
    }
}
