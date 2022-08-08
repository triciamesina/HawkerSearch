using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HawkerSearch.Domain
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        protected readonly HawkerContext _hawkerContext;

        public BaseRepository(HawkerContext dbContext)
        {
            _hawkerContext = dbContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _hawkerContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _hawkerContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            _hawkerContext.Set<T>().Add(entity);
            await _hawkerContext.SaveChangesAsync();
            return entity;
        }

        public async Task BulkAddAsync(IEnumerable<T> entities)
        {
            await _hawkerContext.BulkInsertAsync<T>(entities);
        }

        public async Task UpdateAsync(T entity)
        {
            _hawkerContext.Entry(entity).State = EntityState.Modified;
            await _hawkerContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _hawkerContext.Set<T>().Remove(entity);
            await _hawkerContext.SaveChangesAsync();
        }
    }
}
