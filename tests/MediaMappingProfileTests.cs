using System;
using AutoMapper;
using Epinova.IssuuMedia;
using Xunit;

namespace Epinova.IssuuMediaTests
{
    public class MediaMappingProfileTests
    {
        private readonly MapperConfiguration _config;
        private readonly IMapper _mapper;

        public MediaMappingProfileTests()
        {
            _config = new MapperConfiguration(cfg => { cfg.AddProfile<MediaMappingProfile>(); });
            _mapper = _config.CreateMapper();
        }

        [Fact]
        public void AllowNullCollections_IsFalse()
        {
            var profile = new MediaMappingProfile();

            Assert.False(profile.AllowNullCollections);
        }

        [Fact]
        public void AutomapperConfiguration_IsValid()
        {
            _config.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_MediaDocumentDto_CorrectDocumentName()
        {
            var src = new MediaDocumentDto { name = Factory.GetString() };

            var dest = _mapper.Map<MediaDocument>(src);

            Assert.Equal(src.name, dest.DocumentName);
        }

        [Fact]
        public void Map_MediaDocumentDto_CorrectPublishDate()
        {
            var src = new MediaDocumentDto { publishDate = DateTime.UtcNow };

            var dest = _mapper.Map<MediaDocument>(src);

            Assert.Equal(src.publishDate, dest.PublishedOn);
        }

        [Fact]
        public void Map_MediaDocumentEmbedDto_CorrectCreatedDate()
        {
            var src = new MediaDocumentEmbedDto { created = DateTime.UtcNow };

            var dest = _mapper.Map<MediaDocumentEmbed>(src);

            Assert.Equal(src.created, dest.CreatedOn);
        }
    }
}