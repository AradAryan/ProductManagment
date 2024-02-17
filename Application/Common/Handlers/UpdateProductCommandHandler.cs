using Application.Common.Commands;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;

namespace Application.Common.Handlers
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, bool>
    {
        public IProductRepository ProductRepository { get; set; }
        public IMapper Mapper { get; set; }

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            Mapper = mapper;
            ProductRepository = productRepository;
        }

        public async Task<bool> HandleAsync(UpdateProductCommand command)
        {
            var product = Mapper.Map<Product>(command.Product);
            return await ProductRepository.UpdateAsync(product);
        }
    }
}