using AutoMapper;

namespace Application.Services.Implementations.Base
{
    public class BaseService(IMapper mapper)
    {
        public IMapper Mapper { get; set; } = mapper;
    }

}
