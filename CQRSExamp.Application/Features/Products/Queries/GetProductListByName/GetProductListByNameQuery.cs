using CQRSExamp.Application.Features.Products.ViewModels;
using CQRSExamp.Domain;
using MediatR;

namespace CQRSExamp.Application.Features.Products.Queries.GetProductListByName;

public class GetProductListByNameQuery : IRequest<IEnumerable<ProductVM>>
{
    public string Name { get; set; } = null!;

}
