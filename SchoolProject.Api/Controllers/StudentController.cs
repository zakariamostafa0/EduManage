using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Data.AppMetaData;
using SchoolProject.Data.Entities;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet(Router.StudentRouting.List)]
        public async Task<IActionResult> GetStudentsListAsync()
        {
            var students = await _mediator.Send(new GetStudentListQuery());
            return Ok(students);
        }

        [HttpGet(Router.StudentRouting.GetById)]
        public async Task<IActionResult> GetStudentsByIdAsync([FromRoute]int id)
        {
            var students = await _mediator.Send(new GetStudentByIdQuery(id));
            return Ok(students);
        }

        [HttpPost(Router.StudentRouting.Create)]
        public async Task<IActionResult> CreateStudentAsyns([FromBody]AddStudentCommand command)
        {
            var students = await _mediator.Send(command);
            return Ok(students);
        }
    }
}
