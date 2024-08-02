using NodinSoft.Common.Enum;
using NodinSoft.Common.Model;
using NodinSoft.Entities.ProductManagement;
using NodinSoft.Interfaces.ProductManagement;
using MediatR;
using NodinSoft.Application.ProductManagement.Commands;

namespace NodinSoft.Application.CommandHandlers.ProductManagement
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, MethodExecutionInfo>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<MethodExecutionInfo> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            MethodExecutionInfo result = new MethodExecutionInfo();
            try
            {
                Product? product = await _productRepository.GetByIdAsync(request.Id);
                if (product == null)
                {
                    result.Result = null;
                    result.Success = false;
                    result.Status = ApiStatus.NotFound;
                    result.Message = "The product with this Id was not found";
                    return result;
                }

                if (product.UserId != request.UserId)
                {
                    result.Result = null;
                    result.Success = false;
                    result.Status = ApiStatus.Unauthorized;
                    result.Message = "Only creator of the product can delete it";
                    return result;
                }

                await _productRepository.DeleteAsync(product);

                result.Success = true;
                result.Status = ApiStatus.OK;
                result.Result = 1;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Success = false;
                result.Status = ApiStatus.InternalServerError;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}