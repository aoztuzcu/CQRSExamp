using MediatR;

namespace CQRSExamp.Application.Features.Products.Commonds.ProductCreate;

public class ProductCreateCommand : IRequest<bool>
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }

}
