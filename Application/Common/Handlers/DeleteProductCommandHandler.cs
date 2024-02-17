using Application.Common.Commands;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;

namespace Application.Common.Handlers
{
    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, bool>
    {
        public IProductRepository ProductRepository { get; set; }
        public IMapper Mapper { get; set; }

        public DeleteProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            Mapper = mapper;
            ProductRepository = productRepository;
        }

        public async Task<bool> HandleAsync(DeleteProductCommand command)
        {
            return await ProductRepository.DeleteAsync(command.Id);
        }
    }
}