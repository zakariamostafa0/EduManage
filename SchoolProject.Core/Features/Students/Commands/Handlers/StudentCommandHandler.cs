﻿
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler :ResponseHandler, IRequestHandler<AddStudentCommand, Response<string>>
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
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            //mapping Between request and student
            var student = _mapper.Map<Student>(request);
            //add
            var result = await _studentService.AddStudentAsync(student);
            //check condition and return Response
            if (result == "Exists")
                return UnprocessableEntity<string>("Name is Exists!");
            else if (result == "Success")
                return Created<string>("Added Successfully!");
            else
                return BadRequest<string>();
        }
        #endregion

    }
}
