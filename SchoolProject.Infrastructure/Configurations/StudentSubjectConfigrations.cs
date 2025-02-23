namespace SchoolProject.Infrastructure.Configurations
{
    internal class StudentSubjectConfigrations : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {
            builder.HasKey(x => new { x.SubId, x.StudId });

            builder.HasOne(ds => ds.Student)
            .WithMany(d => d.StudentSubjects)
            .HasForeignKey(ds => ds.StudId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ds => ds.Subject)
            .WithMany(d => d.StudSubjects)
            .HasForeignKey(ds => ds.SubId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
