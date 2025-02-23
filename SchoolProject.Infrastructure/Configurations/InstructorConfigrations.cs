namespace SchoolProject.Infrastructure.Configurations
{
    internal class InstructorConfigrations : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasOne(x => x.SuperVisor)
               .WithMany(x => x.Instructors)
               .HasForeignKey(x => x.SupervisorId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
