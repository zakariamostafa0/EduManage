
using SchoolProject.Core.Features.Students.Commands.Models;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler, IRequestHandler<AddStudentCommand, Response<AddStudentCommand>>
                                                        , IRequestHandler<EditStudentCommand, Response<EditStudentCommand>>
                                                        , IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Construcors
        public StudentCommandHandler(IStudentService studentService,
                                    IMapper mapper,
                                    IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _localizer = localizer;
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

        public async Task<Response<EditStudentCommand>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdAsync(request.Id);
            if (student is null)
                return BadRequest<EditStudentCommand>("Student not found!");

            // Update existing student instead of creating a new one
            _mapper.Map(request, student);
            //OR var studnetmapper = _mapper.Map<Student>(request);
            var result = await _studentService.EditStudentAsync(student);

            //check condition and return Response
            if (!result)
                return BadRequest<EditStudentCommand>();

            //var updatedStudent = await _studentService.GetStudentByIdAsync(request.Id);
            var studentCommand = _mapper.Map<EditStudentCommand>(student);
            return Success<EditStudentCommand>(studentCommand);
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            if (!await _studentService.DeleteStudentAsync(request.Id))
                return BadRequest<string>("Student not found!");
            return Deleted<string>($"The student with ID: {request.Id} was deleted successfully!");

        }
        #endregion

    }
}
