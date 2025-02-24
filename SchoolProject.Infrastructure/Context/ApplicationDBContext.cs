using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using System.Reflection;

namespace SchoolProject.Infrastructure.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext()
        {
        }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<InstructorSubject> InstructorSubjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<DepartmentSubject> DepartmetSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
