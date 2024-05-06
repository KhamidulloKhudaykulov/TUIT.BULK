using Bulk.DataAccess.Contexts;
using Bulk.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bulk.DataAccess.Repositrories;

public class Repository<T> : IRepository<T> where T : Auditable
{
    private readonly AppDbContext context;
    private readonly DbSet<T> set;
    public Repository(AppDbContext context)
    {
        this.context = context;
        this.set = context.Set<T>();
    }
    public async ValueTask<T> InsertAsync(T entity)
    {
        return (await set.AddAsync(entity)).Entity;
    }

    public async ValueTask<T> UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        return await Task.FromResult(set.Update(entity).Entity);
    }

    public async ValueTask<T> DropAsync(T entity)
    {
        return await Task.FromResult(set.Remove(entity).Entity);
    }

    public async ValueTask<T> DeleteAsync(T entity)
    {
        return await Task.FromResult(set.Remove(entity).Entity);
    }

    public async ValueTask<T> SelectAsync(Expression<Func<T, bool>> expression, string[] includes = null)
    {
        var query = set.Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.FirstOrDefaultAsync();
    }

    public async ValueTask<IEnumerable<T>> SelectAllAsEnumerable(Expression<Func<T, bool>> expression = null,
        string[] includes = null,
        bool isTracked = true)
    {
        var query = expression is null ? set : set.Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        if (!isTracked)
            query.AsNoTracking();

        return await query.ToListAsync();
    }

    public IQueryable<T> SelectAllAsQueryable(Expression<Func<T, bool>> expression,
        string[] includes = null,
        bool isTracked = true)
    {
        var query = expression is null ? set : set.Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        if (!isTracked)
            query.AsNoTracking();

        return query;
    }
}
