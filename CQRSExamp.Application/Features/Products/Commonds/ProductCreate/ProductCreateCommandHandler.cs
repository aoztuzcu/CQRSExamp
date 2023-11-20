using AutoMapper;
using CQRSExamp.Application.Contracts.Persistance.Repositories;
using CQRSExamp.Domain;
using MediatR;

namespace CQRSExamp.Application.Features.Products.Commonds.ProductCreate;

public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, bool>
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;
    public ProductCreateCommandHandler(IProductRepository productRepository , IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }
    public async Task<bool> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        var newEntity = mapper.Map<Product>(request);
        return await productRepository.AddAsync(newEntity, cancellationToken);
    }
}
