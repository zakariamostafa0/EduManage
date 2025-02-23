namespace SchoolProject.Infrastructure.Configurations
{
    internal class InstructorSubjectConfigrations : IEntityTypeConfiguration<InstructorSubject>
    {
        public void Configure(EntityTypeBuilder<InstructorSubject> builder)
        {
            builder.HasKey(x => new { x.SubId, x.InsId });

            builder.HasOne(ds => ds.Instructor)
            .WithMany(d => d.InstSubjects)
            .HasForeignKey(ds => ds.InsId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ds => ds.Subject)
            .WithMany(d => d.InstSubjects)
            .HasForeignKey(ds => ds.SubId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
