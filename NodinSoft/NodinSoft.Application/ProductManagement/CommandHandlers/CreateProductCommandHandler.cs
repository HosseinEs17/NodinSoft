using NodinSoft.Domain.ProductManagement; // Corrected
using NodinSoft.Application.ProductManagement.Interfaces; // Corrected
using MediatR;
using NodinSoft.Application.ProductManagement.Commands;

namespace NodinSoft.Application.ProductManagement.CommandHandlers // Corrected namespace
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
            // Ensure required fields for constructor are not null
            if (request.Name == null)
                throw new ArgumentNullException(nameof(request.Name), "Product name cannot be null for creation.");
            if (request.UserId == null)
                throw new ArgumentNullException(nameof(request.UserId), "UserId cannot be null for creation.");
            if (request.ManufactureEmail == null)
                throw new ArgumentNullException(nameof(request.ManufactureEmail), "ManufactureEmail cannot be null for creation.");
            if (request.ManufacturePhone == null)
                throw new ArgumentNullException(nameof(request.ManufacturePhone), "ManufacturePhone cannot be null for creation.");

            var product = new Product(
                request.Name,
                DateTime.UtcNow, // Assuming ProduceDate is now for new products
                request.UserId.Value,
                request.ManufactureEmail,
                request.ManufacturePhone
            );

            // Product constructor sets IsAvailable to true by default.
            // If request.IsAvailable is explicitly false, then mark as unavailable.
            if (request.IsAvailable.HasValue && !request.IsAvailable.Value)
            {
                product.MarkAsUnavailable();
            }

            await _productRepository.AddAsync(product);

            return product.Id;
        }

    }
}