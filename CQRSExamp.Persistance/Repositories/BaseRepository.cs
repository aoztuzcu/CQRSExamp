using CQRSExamp.Application.Contracts.Persistance.Repositories;
using CQRSExamp.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CQRSExamp.Persistance.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity, new()
{
    protected readonly CQRSDbContext dataContext;
    public BaseRepository(CQRSDbContext dataContext)
        => this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));

    public async Task<bool> AddAsync(TEntity entity, CancellationToken token)
    {
        await dataContext.Set<TEntity>().AddAsync(entity, token);
        await dataContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, CancellationToken token)
        => await dataContext.Set<TEntity>().Where(expression).ToListAsync(token);

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken token)
        => await dataContext.Set<TEntity>().FirstOrDefaultAsync(expression, token) ?? throw new Exception($"{nameof(TEntity)} Data Not Found!");
}
