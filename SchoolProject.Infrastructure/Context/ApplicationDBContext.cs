using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
        {
        }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<DepartmentSubject> DepartmetSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartmentSubject>()
                .HasKey(x => new { x.SubId, x.DepId });
            modelBuilder.Entity<StudentSubject>()
                .HasKey(x => new { x.SubId, x.StudId });
            modelBuilder.Entity<InstructorSubject>()
                .HasKey(x => new { x.SubId, x.InsId });

            modelBuilder.Entity<Instructor>()
                .HasOne(x => x.SuperVisor)
                .WithMany(x => x.Instructors)
                .HasForeignKey(x => x.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Department>()
               .HasOne(x => x.Instructor)
               .WithOne(x => x.DepartmentManager)
               .HasForeignKey<Department>(x => x.InsManager)
               .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
