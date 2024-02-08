using Application.Dtos;
using Application.Services.Implementations.Base;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;


namespace Application.Services.Implementations
{
    public class ProductService : BaseService, IProductService
    {
        public IProductRepository ProductRepository { get; set; }

        public ProductService(IProductRepository productRepository, IMapper mapper) : base(mapper)
        {
            ProductRepository = productRepository;
        }

        public async Task<bool> CreateProductAsync(ProductDto product)
        {
            var data = Mapper.Map<ProductDto, Product>(product);
            return await ProductRepository.CreateAsync(data);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var res = await ProductRepository.GetByIdAsync(id);
            return Mapper.Map<Product, ProductDto>(res);
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var res = await ProductRepository.GetAllAsync();
            return Mapper.Map<List<Product>, List<ProductDto>>(res);
        }

        public async Task<bool> UpdateProductAsync(ProductDto product)
        {
            var res = Mapper.Map<ProductDto, Product>(product);
            return await ProductRepository.UpdateAsync(res);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await ProductRepository.DeleteAsync(id);
        }
    }
}