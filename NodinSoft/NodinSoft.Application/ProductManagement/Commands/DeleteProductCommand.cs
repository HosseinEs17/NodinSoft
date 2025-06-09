using NodinSoft.Common.Models; // Corrected
using MediatR;

namespace NodinSoft.Application.ProductManagement.Commands
{
    public class DeleteProductCommand : IRequest<MethodExecutionInfo>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }
}