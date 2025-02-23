namespace SchoolProject.Core.Features.Department.Queries.Results
{
    public class GetDepartmentByIdResponse
    {
        public int DID { get; set; }
        public string DName { get; set; }
        public string? ManagerName { get; set; }
        public PaginatedResult<StudentResponse>? StudentListPagination { get; set; }
        public List<InstructorResponse>? InstructorList { get; set; }
        public List<SubjectsResponse>? SubjectList { get; set; }
    }

    public class StudentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StudentResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    public class InstructorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class SubjectsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
