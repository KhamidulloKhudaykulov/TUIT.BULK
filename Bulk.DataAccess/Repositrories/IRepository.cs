using Bulk.Domain.Commons;
using System.Linq.Expressions;

namespace Bulk.DataAccess.Repositrories;

public interface IRepository<T> where T : Auditable
{
    ValueTask<T> InsertAsync(T entity);
    ValueTask<T> UpdateAsync(T entity);
    ValueTask<T> DeleteAsync(T entity);
    ValueTask<T> DropAsync(T entity);
    ValueTask<T> SelectAsync(Expression<Func<T, bool>> expression, string[] includes = null);
    ValueTask<IEnumerable<T>> SelectAllAsEnumerable(
        Expression<Func<T, bool>> expression = null,
        string[] includes = null,
        bool isTracked = true);
    IQueryable<T> SelectAllAsQueryable(
        Expression<Func<T, bool>> expression,
        string[] includes = null,
        bool isTracked = true);
}
