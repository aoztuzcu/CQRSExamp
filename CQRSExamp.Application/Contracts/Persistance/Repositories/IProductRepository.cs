using CQRSExamp.Domain;

namespace CQRSExamp.Application.Contracts.Persistance.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<IEnumerable<Product>> GetAllByNameAsync(string name, CancellationToken token);
}
