using NodinSoft.Entities.ProductManagement;
using MediatR;

namespace NodinSoft.Application.ProductManagement.Queries
{
    public class GetFilteredProductsByUserIdQuery : IRequest<IEnumerable<Product>>
    {
        public int UserId { get; set; }
    }
}