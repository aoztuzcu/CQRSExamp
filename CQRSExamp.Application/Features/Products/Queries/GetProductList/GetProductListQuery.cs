using CQRSExamp.Application.Features.Products.ViewModels;
using MediatR;

namespace CQRSExamp.Application.Features.Products.Queries.GetProductList;

public class GetProductListQuery : IRequest<IEnumerable<ProductVM>>
{
}
