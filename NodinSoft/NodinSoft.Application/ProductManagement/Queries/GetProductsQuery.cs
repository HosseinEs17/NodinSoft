using NodinSoft.Entities.ProductManagement;
using MediatR;

namespace NodinSoft.Application.ProductManagement.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}