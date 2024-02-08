using AutoMapper;

namespace Application.Implementations.Base
{
    public class BaseService(IMapper mapper)
    {
        public IMapper Mapper { get; set; } = mapper;
    }

}
