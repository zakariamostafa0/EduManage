namespace SchoolProject.Data.Entities.Procedure
{
    public class DepartmentStudentsCountProc
    {
        public int DID { get; set; }
        public string? DName { get; set; }
        public int StudentsCount { get; set; }
    }
    public class DepartmentStudentsCountProcParmeters
    {
        public int Did { get; set; } = 0;
    }
}
