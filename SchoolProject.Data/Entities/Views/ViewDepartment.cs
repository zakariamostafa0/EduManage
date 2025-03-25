using Microsoft.EntityFrameworkCore;

namespace SchoolProject.Data.Entities.Views
{
    [Keyless]
    public class ViewDepartment
    {
        public int DID { get; set; }
        public string? DName { get; set; }
        public int StudentsCount { get; set; }
    }
}
