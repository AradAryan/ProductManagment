using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IProductRepository
    {
        Task<bool> CreateAsync(Product product);
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
    }

}
