using NodinSoft.Domain.ProductManagement; // Corrected
using MediatR;
using NodinSoft.Application.ProductManagement.Dtos; // Added DTO using

namespace NodinSoft.Application.ProductManagement.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductDto>> // Changed to ProductDto
    {
    }
}