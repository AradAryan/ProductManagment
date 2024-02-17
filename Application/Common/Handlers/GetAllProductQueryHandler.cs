using Application.Common.Commands;
using Application.Dtos;
using AutoMapper;
using Infrastructure.Repositories.Interfaces;

namespace Application.Common.Handlers
{
    public class GetAllProductQueryHandler : IQueryHandler<GetAllProductsQuery, List<ProductDto>>
    {
        public IProductRepository ProductRepository { get; set; }
        public IMapper Mapper { get; set; }

        public GetAllProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            Mapper = mapper;
            ProductRepository = productRepository;
        }

        public async Task<List<ProductDto>> HandleAsync(GetAllProductsQuery query)
        {
            var products = await ProductRepository.GetAllAsync();
            var data = Mapper.Map<List<ProductDto>>(products);
            return data;
        }
    }
}