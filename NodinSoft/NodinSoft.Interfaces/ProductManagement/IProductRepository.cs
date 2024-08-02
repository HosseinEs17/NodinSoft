using NodinSoft.Entities.ProductManagement;

namespace NodinSoft.Interfaces.ProductManagement
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product product);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetByUserIdAsync(int userId);
        Task<Product?> GetByIdAsync(int Id);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}