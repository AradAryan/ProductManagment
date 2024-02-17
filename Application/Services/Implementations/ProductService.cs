using Application.Common.Commands;
using Application.Common.Handlers;
using Application.Dtos;
using Application.Services.Implementations.Base;
using Application.Services.Interfaces;
using AutoMapper;

namespace Application.Services.Implementations
{
    public class ProductService : BaseService, IProductService
    {

        public ICommandHandler<CreateProductCommand, bool> CreateProductCommandHandler { get; set; }

        public IQueryHandler<GetProductByIdQuery, ProductDto> GetProductByIdQueryHandler { get; set; }

        public IQueryHandler<GetAllProductsQuery, List<ProductDto>> GetAllProductsQueryHandler { get; set; }

        public ICommandHandler<UpdateProductCommand, bool> UpdateProductCommandHandler { get; set; }

        public ICommandHandler<DeleteProductCommand, bool> DeleteProductCommandHandler { get; set; }

        public ProductService(
            ICommandHandler<CreateProductCommand, bool> createProductCommandHandler,
            IQueryHandler<GetProductByIdQuery, ProductDto> getProductByIdQueryHandler,
            IQueryHandler<GetAllProductsQuery, List<ProductDto>> getAllProductsQueryHandler,
            ICommandHandler<UpdateProductCommand, bool> updateProductCommandHandler,
            ICommandHandler<DeleteProductCommand, bool> deleteProductCommandHandler)
        {
            CreateProductCommandHandler = createProductCommandHandler;
            GetProductByIdQueryHandler = getProductByIdQueryHandler;
            GetAllProductsQueryHandler = getAllProductsQueryHandler;
            UpdateProductCommandHandler = updateProductCommandHandler;
            DeleteProductCommandHandler = deleteProductCommandHandler;
        }

        public async Task<bool> CreateProductAsync(ProductDto product)
        {
            var command = new CreateProductCommand { Product = product };
            return await CreateProductCommandHandler.HandleAsync(command);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var query = new GetProductByIdQuery { Id = id };
            return await GetProductByIdQueryHandler.HandleAsync(query);
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var query = new GetAllProductsQuery();
            return await GetAllProductsQueryHandler.HandleAsync(query);
        }

        public async Task<bool> UpdateProductAsync(ProductDto product)
        {
            var command = new UpdateProductCommand { Product = product };
            return await UpdateProductCommandHandler.HandleAsync(command);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var command = new DeleteProductCommand { Id = id };
            return await DeleteProductCommandHandler.HandleAsync(command);
        }
    }
}