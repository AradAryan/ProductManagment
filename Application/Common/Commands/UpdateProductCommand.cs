using Application.Dtos;


namespace Application.Common.Commands
{
    public class UpdateProductCommand : ICommand<bool>
    {
        public ProductDto Product { get; set; }
    }
}
