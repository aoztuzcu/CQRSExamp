using AutoMapper;
using CQRSExamp.Application.Contracts.Persistance.Repositories;
using CQRSExamp.Application.Features.Products.ViewModels;
using MediatR;

namespace CQRSExamp.Application.Features.Products.Queries.GetProductList;

public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IEnumerable<ProductVM>>
{
    private readonly IProductRepository repository;
    private readonly IMapper mapper;

    public GetProductListQueryHandler(IProductRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }
    public async Task<IEnumerable<ProductVM>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
       => mapper.Map<IEnumerable<ProductVM>>(await repository.GetAllAsync(f => f != null, cancellationToken));
}

