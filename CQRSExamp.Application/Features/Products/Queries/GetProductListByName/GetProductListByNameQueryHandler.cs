using AutoMapper;
using CQRSExamp.Application.Contracts.Persistance.Repositories;
using CQRSExamp.Application.Features.Products.ViewModels;
using MediatR;

namespace CQRSExamp.Application.Features.Products.Queries.GetProductListByName;

public class GetProductListByNameQueryHandler : IRequestHandler<GetProductListByNameQuery, IEnumerable<ProductVM>>
{
    private readonly IProductRepository repository;
    private readonly IMapper mapper;

    public GetProductListByNameQueryHandler(IProductRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }
    public async Task<IEnumerable<ProductVM>> Handle(GetProductListByNameQuery request, CancellationToken cancellationToken)
    {
        var data = await repository.GetAllByNameAsync(request.Name, cancellationToken);
        return mapper.Map<IEnumerable<ProductVM>>(data);
    }
}
