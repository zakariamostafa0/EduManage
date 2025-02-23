namespace SchoolProject.Infrastructure.Configurations
{
    internal class DepartmentSubjectConfigrations : IEntityTypeConfiguration<DepartmentSubject>
    {
        public void Configure(EntityTypeBuilder<DepartmentSubject> builder)
        {
            builder.HasKey(x => new { x.SubId, x.DepId });

            builder.HasOne(ds => ds.Department)
            .WithMany(d => d.DeptSubjects)
            .HasForeignKey(ds => ds.DepId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ds => ds.Subject)
            .WithMany(d => d.DeptSubjects)
            .HasForeignKey(ds => ds.SubId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
