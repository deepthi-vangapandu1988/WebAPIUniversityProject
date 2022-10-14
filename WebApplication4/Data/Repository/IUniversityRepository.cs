using System.Linq.Expressions;

namespace WebApplication4.Data.Repository
{
    public interface IUniversityRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> filter);
        Task CreateAsync(T model);
        Task UpdateAsync(T model);
        Task DeleteAsync(T model);
        Task CommitChangesAsync();
        Task<List<T>> ExecSql(string departmentName);
    }
}
