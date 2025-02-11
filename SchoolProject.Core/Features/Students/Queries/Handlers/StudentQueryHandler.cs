
namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
    public class StudentQueryHandler : ResponseHandler, 
                                        IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>,
                                        IRequestHandler<GetStudentByIdQuery, Response<GetStudentListResponse>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public StudentQueryHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        #endregion

        #region Handles Methods
        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentService.GetStudentsListAsync();
            var studentsDTO = _mapper.Map<List<GetStudentListResponse>>(students);
            return Success(studentsDTO);
        }

        public async Task<Response<GetStudentListResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdAsync(request.Id);
            if (student == null)
                return NotFound<GetStudentListResponse>("NO student was found!!");
            var studentResponse = _mapper.Map<GetStudentListResponse>(student);
            return Success(studentResponse);
        }
        #endregion
    }
}
