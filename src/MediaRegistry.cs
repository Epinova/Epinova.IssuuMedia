using AutoMapper;
using StructureMap;

namespace Epinova.IssuuMedia
{
    public class MediaRegistry : Registry
    {
        public MediaRegistry()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile(new MediaMappingProfile()); });
            mapperConfiguration.CompileMappings();

            For<IMediaService>().Use<MediaService>().Ctor<IMapper>().Is(mapperConfiguration.CreateMapper());
        }
    }
}
