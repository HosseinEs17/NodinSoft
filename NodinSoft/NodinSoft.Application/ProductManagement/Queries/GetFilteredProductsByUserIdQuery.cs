using NodinSoft.Domain.ProductManagement; // Corrected
using MediatR;
using NodinSoft.Application.ProductManagement.Dtos; // Added DTO using

namespace NodinSoft.Application.ProductManagement.Queries
{
    public class GetFilteredProductsByUserIdQuery : IRequest<IEnumerable<ProductDto>> // Changed to ProductDto
    {
        public int UserId { get; set; }
    }
}