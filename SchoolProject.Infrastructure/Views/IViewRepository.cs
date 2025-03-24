namespace SchoolProject.Infrastructure.Views
{
    public interface IViewRepository<T> : IGenericRepositoryAsync<T> where T : class
    {
    }
}
