using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WebApplication4.Data.Repository
{
    public class UniversityRepository<T> : IUniversityRepository<T> where T : class
    {
        private readonly UniversityDbContext _db;
        private DbSet<T> _dbSet;
        public UniversityRepository(UniversityDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }
        public async Task CommitChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task CreateAsync(T model)
        {
            _dbSet.Add(model);
            await CommitChangesAsync();
        }

        public async Task DeleteAsync(T model)
        {
            _dbSet.Remove(model);
            await CommitChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.Where(filter).ToListAsync();
        }

        public async Task UpdateAsync(T model)
        {
            _dbSet.Update(model);
            await CommitChangesAsync();
        }
        public async Task<List<T>> ExecSql(string departmentName)
        {
            var param = new SqlParameter("@deptname", departmentName);
            //var result = _dbSet.FromSqlInterpolated($"exec getstudentsbydept {departmentName}").AsNoTracking().ToList();
            var result = await _dbSet.FromSqlRaw("exec getstudentsbydept {0}", param).AsNoTracking().ToListAsync();
            return result;
        }
    }
}
