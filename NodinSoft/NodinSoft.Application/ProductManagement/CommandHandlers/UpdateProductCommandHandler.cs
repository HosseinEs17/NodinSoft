using NodinSoft.Common.Enums; // Corrected to Enums
using NodinSoft.Common.Models; // Corrected to Models
using NodinSoft.Domain.ProductManagement; // Corrected
using NodinSoft.Application.ProductManagement.Interfaces; // Corrected
using MediatR;
using NodinSoft.Application.ProductManagement.Commands;

namespace NodinSoft.Application.ProductManagement.CommandHandlers // Corrected namespace
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, MethodExecutionInfo>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<MethodExecutionInfo> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
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
                    result.Message = "Only creator of the product can update it";
                    return result;
                }

                // product.Name has a public setter, so direct assignment is okay if that's the design.
                // If Name should also be part of a method like UpdateDetails, this would change.
                if (request.Name != null && product.Name != request.Name) // Only update if changed and not null
                {
                    product.Name = request.Name;
                }

                if (request.IsAvailable && !product.IsAvailable)
                {
                    product.MarkAsAvailable();
                }
                else if (!request.IsAvailable && product.IsAvailable)
                {
                    product.MarkAsUnavailable();
                }

                await _productRepository.UpdateAsync(product);

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