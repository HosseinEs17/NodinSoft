using NodinSoft.Domain.ProductManagement; // Corrected
using NodinSoft.Application.ProductManagement.Interfaces; // Corrected
using MediatR;
using NodinSoft.Application.ProductManagement.Queries;
using NodinSoft.Application.ProductManagement.Dtos; // Added DTO using
using System.Linq; // Added for .Select

namespace NodinSoft.Application.ProductManagement.QueryHandlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>> // Changed to ProductDto
    {
        private readonly IProductRepository _productRepository;
        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken) // Changed to ProductDto
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                ProduceDate = p.ProduceDate,
                ManufacturePhone = p.ManufacturePhone,
                ManufactureEmail = p.ManufactureEmail,
                IsAvailable = p.IsAvailable,
                UserId = p.UserId
            }).ToList();
        }
    }
}