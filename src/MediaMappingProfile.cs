using AutoMapper;

namespace Epinova.IssuuMedia
{
    internal class MediaMappingProfile : Profile
    {
        public MediaMappingProfile()
        {
            AllowNullCollections = false;

            CreateMap<MediaDocumentDto, MediaDocument>()
                .ForMember(dest => dest.DocumentName, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.PublishedOn, opt => opt.MapFrom(src => src.publishDate));

            CreateMap<MediaDocumentEmbedDto, MediaDocumentEmbed>()
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.created))
                .ForMember(dest => dest.Document, opt => opt.Ignore());
        }
    }
}