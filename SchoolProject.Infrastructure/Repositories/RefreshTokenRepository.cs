using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
    {
        #region Filds
        private readonly DbSet<UserRefreshToken> _userRefreshToken;

        #endregion

        #region Constructors
        public RefreshTokenRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _userRefreshToken = dbContext.Set<UserRefreshToken>();
        }

        #endregion

        #region Handles Methods

        #endregion
    }
}
