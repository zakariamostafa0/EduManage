using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.Abstracts
{
    public interface IRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
    {
    }
}
