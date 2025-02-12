namespace SchoolProject.Core.Features.Students.Queries.Results
{
    public class AddStudentCommand : IRequest<Response<AddStudentCommand>>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int DepartmentId { get; set; }
    }
}
