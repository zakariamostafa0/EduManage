namespace SchoolProject.Infrastructure.Configurations
{
    internal class DepartmentConfigrations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasMany(x => x.Students)
            .WithOne(x => x.Department)
            .HasForeignKey(x => x.DID)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Instructor)
            .WithOne(x => x.DepartmentManager)
            .HasForeignKey<Department>(x => x.InsManager)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
