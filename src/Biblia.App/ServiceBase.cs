using AutoMapper;

namespace Biblia.App
{
    public class ServiceBase
    {
        public IMapper Mapper { get; private set; }

        public ServiceBase()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {

            });

            Mapper = mapperConfig.CreateMapper();
        }
    }
}
