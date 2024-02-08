using Application.Dtos;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(ProductDto product);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<bool> UpdateProductAsync(ProductDto product);
        Task<bool> DeleteProductAsync(int id);
    }
}
