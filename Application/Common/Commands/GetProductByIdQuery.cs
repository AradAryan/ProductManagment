using Application.Dtos;

namespace Application.Common.Commands
{
    public class GetProductByIdQuery : IQuery<ProductDto>
    {
        public int Id { get; set; }
    }
}
