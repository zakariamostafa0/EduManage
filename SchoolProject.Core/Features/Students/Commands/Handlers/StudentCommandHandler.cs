
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler, IRequestHandler<AddStudentCommand, Response<AddStudentCommand>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        #endregion

        #region Construcors
        public StudentCommandHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        #endregion

        #region Handle Methods
        public async Task<Response<AddStudentCommand>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            //mapping Between request and student
            var student = _mapper.Map<Student>(request);
            //add
            var result = await _studentService.AddStudentAsync(student);
            var studentCommand = _mapper.Map<AddStudentCommand>(result);
            //check condition and return Response
            if (result is null)
                return BadRequest<AddStudentCommand>();
            return Created<AddStudentCommand>(studentCommand);

        }
        #endregion

    }
}
