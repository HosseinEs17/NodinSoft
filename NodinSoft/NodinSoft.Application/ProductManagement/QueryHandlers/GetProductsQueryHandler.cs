using NodinSoft.Entities.ProductManagement;
using NodinSoft.Interfaces.ProductManagement;
using MediatR;
using NodinSoft.Application.ProductManagement.Queries;

namespace NodinSoft.Application.ProductManagement.QueryHandlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllAsync();
        }
    }
}