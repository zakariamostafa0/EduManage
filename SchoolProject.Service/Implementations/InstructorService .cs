using Microsoft.AspNetCore.Http;
using SchoolProject.Infrastructure.Abstracts.Functions;

namespace SchoolProject.Service.Implementations
{
    public class InstructorService : IInstructorService
    {
        #region Fileds
        private readonly IInstructorFunctionsRepository _instructorFunctionsRepository;
        private readonly IInstructorsRepository _instructorsRepository;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion
        #region Constructors
        public InstructorService(
                                 IInstructorFunctionsRepository instructorFunctionsRepository,
                                 IInstructorsRepository instructorsRepository,
                                 IFileService fileService,
                                 IHttpContextAccessor httpContextAccessor)
        {
            _instructorFunctionsRepository = instructorFunctionsRepository;
            _instructorsRepository = instructorsRepository;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }



        #endregion
        #region Handle Functions
        public async Task<decimal> GetSalarySummationOfInstructor()
        {
            decimal result = 0;
            result = _instructorFunctionsRepository.GetSalarySummationOfInstructor("Select dbo.GetSalarySummation()");
            return result;
        }

        public async Task<bool> IsNameExist(string name)
        {
            //Check if the name is Exist Or not
            var student = await _instructorsRepository.GetTableNoTracking().Where(x => x.Name.Equals(name)).FirstOrDefaultAsync();
            if (student == null) return false;
            return true;
        }

        public async Task<bool> IsNameExistExcludeSelf(string name, int id)
        {
            //Check if the name is Exist Or not
            var student = await _instructorsRepository.GetTableNoTracking().Where(x => x.Name.Equals(name) & x.InsId != id).FirstOrDefaultAsync();
            if (student == null) return false;
            return true;
        }
        public async Task<string> AddInstructorAsync(Instructor instructor, IFormFile file)
        {
            var context = _httpContextAccessor.HttpContext.Request;
            var baseUrl = context.Scheme + "://" + context.Host;
            var imageUrl = await _fileService.UploadImage("Instructors", file);
            switch (imageUrl)
            {
                case "NoImage": return "NoImage";
                case "FailedToUploadImage": return "FailedToUploadImage";
            }
            instructor.Image = baseUrl + imageUrl;
            try
            {
                await _instructorsRepository.AddAsync(instructor);
                return "Success";
            }
            catch (Exception)
            {
                return "FailedInAdd";
            }
        }
        #endregion
    }
}