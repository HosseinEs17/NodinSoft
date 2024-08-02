using NodinSoft.Entities.ProductManagement;
using NodinSoft.Interfaces.ProductManagement;
using MediatR;
using NodinSoft.Application.ProductManagement.Commands;

namespace NodinSoft.Application.CommandHandlers.ProductManagement
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                ManufacturePhone = request.ManufacturePhone,
                ManufactureEmail = request.ManufactureEmail,
                IsAvailable = request.IsAvailable is null ? true : (bool)request.IsAvailable,
                UserId = (int)request.UserId
            };

            await _productRepository.AddAsync(product);

            return product.Id;
        }

    }
}