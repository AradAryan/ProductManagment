using Application.Common.Commands;
using Application.Dtos;
using AutoMapper;
using Infrastructure.Repositories.Interfaces;

namespace Application.Common.Handlers
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDto>
    {
        public IProductRepository ProductRepository { get; set; }
        public IMapper Mapper { get; set; }

        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            Mapper = mapper;
            ProductRepository = productRepository;
        }

        public async Task<ProductDto> HandleAsync(GetProductByIdQuery query)
        {
            var product = await ProductRepository.GetByIdAsync(query.Id);
            var productDto = Mapper.Map<ProductDto>(product);
            return productDto;
        }
    }
}