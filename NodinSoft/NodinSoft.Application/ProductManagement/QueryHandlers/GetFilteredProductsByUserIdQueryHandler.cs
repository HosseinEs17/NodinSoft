using NodinSoft.Entities.ProductManagement;
using NodinSoft.Interfaces.ProductManagement;
using MediatR;
using NodinSoft.Application.ProductManagement.Queries;

namespace NodinSoft.Application.ProductManagement.QueryHandlers
{
    public class GetFilteredProductsByUserIdQueryHandler : IRequestHandler<GetFilteredProductsByUserIdQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;
        public GetFilteredProductsByUserIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> Handle(GetFilteredProductsByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetByUserIdAsync(request.UserId);
        }
    }
}