using CQRSExamp.Domain.Common;
using System.Linq.Expressions;

namespace CQRSExamp.Application.Contracts.Persistance.Repositories;

public interface IBaseRepository<T> where T : BaseEntity, new()
{
    public Task<bool> AddAsync(T entity, CancellationToken token);
    public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken token);
    public Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken token);
}