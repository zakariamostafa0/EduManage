using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.Configurations
{
    public class RolesConfigrations : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData(
                new ApplicationRole() { Name = "Admin" },
                new ApplicationRole() { Name = "User" }
                );
        }
    }
}
