using Application.Common.Commands;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;

namespace Application.Common.Handlers
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, bool>
    {
        public IProductRepository ProductRepository { get; set; }
        public IMapper Mapper { get; set; }

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            Mapper = mapper;
            ProductRepository = productRepository;
        }

        public async Task<bool> HandleAsync(CreateProductCommand command)
        {
            var product = Mapper.Map<Product>(command.Product);
            return await ProductRepository.CreateAsync(product);
        }
    }
}