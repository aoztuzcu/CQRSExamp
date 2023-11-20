using CQRSExamp.Application.Contracts.Persistance.Repositories;
using CQRSExamp.Domain;

namespace CQRSExamp.Persistance.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(CQRSDbContext dataContext) : base(dataContext)
    {
    }

    public async Task<IEnumerable<Product>> GetAllByNameAsync(string name, CancellationToken token)
         => await base.GetAllAsync(g => g.Name.Contains(name), token);
}
