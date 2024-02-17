using Application.Dtos;

namespace Application.Common.Commands
{
    public class CreateProductCommand : ICommand<bool>
    {
        public ProductDto Product { get; set; }
    }
}
