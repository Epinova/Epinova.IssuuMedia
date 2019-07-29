using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Epinova.IssuuMedia;
using EPiServer.Logging;
using Moq;
using Xunit;

namespace Epinova.IssuuMediaTests
{
    public class MediaServiceTests
    {
        private readonly Mock<ILogger> _logMock;
        private readonly TestableHttpMessageHandler _messageHandler;
        private readonly MediaService _service;


        public MediaServiceTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile(new MediaMappingProfile()); });
            _messageHandler = new TestableHttpMessageHandler();
            _logMock = new Mock<ILogger>();
            MediaService.Client = new HttpClient(_messageHandler) { BaseAddress = new Uri("https://fake.api.uri/") };
            _service = new MediaService(_logMock.Object, mapperConfiguration.CreateMapper());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GetDocumentEmbeds_MissingApiKey_DoesNotCallAPI(string apiKey)
        {
            await _service.GetDocumentEmbedsAsync(apiKey, Factory.GetString());

            Assert.Equal(0, _messageHandler.CallCount());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GetDocumentEmbeds_MissingApiKey_LogWarning(string apiKey)
        {
            await _service.GetDocumentEmbedsAsync(apiKey, Factory.GetString());

            _logMock.VerifyLog(Level.Warning, "Missing API key and/or secret", Times.Once());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GetDocumentEmbeds_MissingApiKey_ReturnsEmptyList(string apiKey)
        {
            MediaDocumentEmbed[] result = await _service.GetDocumentEmbedsAsync(apiKey, Factory.GetString());

            Assert.Empty(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GetDocumentEmbeds_MissingApiSecret_DoesNotCallAPI(string apiSecret)
        {
            await _service.GetDocumentEmbedsAsync(Factory.GetString(), apiSecret);

            Assert.Equal(0, _messageHandler.CallCount());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GetDocumentEmbeds_MissingApiSecret_LogsWarning(string apiSecret)
        {
            await _service.GetDocumentEmbedsAsync(Factory.GetString(), apiSecret);

            _logMock.VerifyLog(Level.Warning, "Missing API key and/or secret", Times.Once());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GetDocumentEmbeds_MissingApiSecret_ReturnsEmptyList(string apiSecret)
        {
            MediaDocumentEmbed[] result = await _service.GetDocumentEmbedsAsync(Factory.GetString(), apiSecret);

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetDocuments_InternalServerError_ReturnsEmpty()
        {
            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            MediaDocument[] result = await _service.GetDocumentsAsync(Factory.GetString(), Factory.GetString());

            Assert.Empty(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GetDocuments_MissingApiKey_DoesNotCallAPI(string apiKey)
        {
            await _service.GetDocumentsAsync(apiKey, Factory.GetString());

            Assert.Equal(0, _messageHandler.CallCount());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GetDocuments_MissingApiKey_LogWarning(string apiKey)
        {
            await _service.GetDocumentsAsync(apiKey, Factory.GetString());

            _logMock.VerifyLog(Level.Warning, "Missing API key and/or secret", Times.Once());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GetDocuments_MissingApiKey_ReturnsEmptyList(string apiKey)
        {
            MediaDocument[] result = await _service.GetDocumentsAsync(apiKey, Factory.GetString());

            Assert.Empty(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GetDocuments_MissingApiSecret_DoesNotCallAPI(string apiSecret)
        {
            await _service.GetDocumentsAsync(Factory.GetString(), apiSecret);

            Assert.Equal(0, _messageHandler.CallCount());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GetDocuments_MissingApiSecret_LogsWarning(string apiSecret)
        {
            await _service.GetDocumentsAsync(Factory.GetString(), apiSecret);

            _logMock.VerifyLog(Level.Warning, "Missing API key and/or secret", Times.Once());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GetDocuments_MissingApiSecret_ReturnsEmptyList(string apiSecret)
        {
            MediaDocument[] result = await _service.GetDocumentsAsync(Factory.GetString(), apiSecret);

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetDocuments_MissingParameter_ReturnsEmptyList()
        {
            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"rsp\":{\"_content\":{\"error\":{\"code\":\"200\",\"message\":\"Required field is missing\",\"field\":\"apiKey\"}},\"stat\":\"fail\"}}")
            });
            MediaDocument[] result = await _service.GetDocumentsAsync(Factory.GetString(), Factory.GetString());

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetDocuments_ParseResultFails_ReturnsEmpty()
        {
            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{ 'Some': 'random', 'unparasable': 'json' }")
            });
            MediaDocument[] result = await _service.GetDocumentsAsync(Factory.GetString(), Factory.GetString());

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetDocuments_ResponseOK_ReturnsDocumentList()
        {
            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    "{\"rsp\":{\"_content\":{\"result\":{\"totalCount\":25,\"startIndex\":0,\"pageSize\":10,\"more\":true,\"_content\":[{\"document\":{\"username\":\"matchfashion\",\"name\":\"ma_perfect_match_dm_feb2016\",\"publicationId\":\"3d17f941e434465403201c19320464ac\",\"revisionId\":\"160311123121\",\"documentId\":\"160311123121-3d17f941e434465403201c19320464ac\",\"title\":\"MATCH Fashion- Perfect Match- Februar 2016\"}},{\"document\":{\"username\":\"matchfashion\",\"name\":\"160309_ma_lookbook_pa__ske_new\",\"publicationId\":\"4483f941803143461b514418cd648785\",\"revisionId\":\"160316075425\",\"documentId\":\"160316075425-4483f941803143461b514418cd648785\",\"title\":\"MATCH Fashion- Ytterjakker- Påske 2016\"}},{\"document\":{\"username\":\"matchfashion\",\"name\":\"160309_ma_lookbook_spiritofmatch_en\",\"publicationId\":\"f6cc879b44c20df762e1e1407ed8e8e6\",\"revisionId\":\"160411083552\",\"documentId\":\"160411083552-f6cc879b44c20df762e1e1407ed8e8e6\",\"title\":\"The Spirit of Match\"}},{\"document\":{\"username\":\"matchfashion\",\"name\":\"160320_ma_lookbook_17_mai_01\",\"publicationId\":\"bb83d7ea5443ffee546fb44d424ebcdb\",\"revisionId\":\"160428112354\",\"documentId\":\"160428112354-bb83d7ea5443ffee546fb44d424ebcdb\",\"title\":\"MATCH Fashion - Tid for feiring\"}},{\"document\":{\"username\":\"matchfashion\",\"name\":\"160527_ma_lookbook_sommer\",\"publicationId\":\"09863d5f799dc7a058e8f70716629c59\",\"revisionId\":\"160608103939\",\"documentId\":\"160608103939-09863d5f799dc7a058e8f70716629c59\",\"title\":\"MATCH Fashion - sommer\"}},{\"document\":{\"username\":\"matchfashion\",\"name\":\"160829_ma_digital-dm_strikk-uke36-3\",\"publicationId\":\"8e9d15cc6670680a327fcc7e15a922c7\",\"revisionId\":\"160907072123\",\"documentId\":\"160907072123-8e9d15cc6670680a327fcc7e15a922c7\",\"title\":\"MATCH - lun høststrikk\"}},{\"document\":{\"username\":\"matchfashion\",\"name\":\"160916_ma_digital-dm_jakker-uke39-4\",\"publicationId\":\"31e0030205da5ef7be064c98a0599c56\",\"revisionId\":\"160927144305\",\"documentId\":\"160927144305-31e0030205da5ef7be064c98a0599c56\",\"title\":\"MATCH - høstens jakketrender\"}},{\"document\":{\"username\":\"matchfashion\",\"name\":\"julens_fineste_gaver_match_2016\",\"publicationId\":\"40f76181d1588566eafc8092ac72aac6\",\"revisionId\":\"161122122053\",\"documentId\":\"161122122053-40f76181d1588566eafc8092ac72aac6\",\"title\":\"Julens fineste gaver!\"}},{\"document\":{\"username\":\"matchfashion\",\"name\":\"160916_ma_digital-dm_jakker-uke39-4_3674e24434c327\",\"publicationId\":\"ff2cb5de7fb0b1dc85345f5855fbf59f\",\"revisionId\":\"170222103913\",\"documentId\":\"170222103913-ff2cb5de7fb0b1dc85345f5855fbf59f\",\"title\":\"MATCH | Vårens buksenyheter\"}},{\"document\":{\"username\":\"matchfashion\",\"name\":\"160916_ma_digital-dm_jakker-uke39-4_192cbc5cfe8250\",\"publicationId\":\"008a8cc2695fece11bf5e482611c2b9c\",\"revisionId\":\"170317090458\",\"documentId\":\"170317090458-008a8cc2695fece11bf5e482611c2b9c\",\"title\":\"MATCH |Vårens jakkenyheter\"}}]}},\"stat\":\"ok\"}}")
            });
            MediaDocument[] result = await _service.GetDocumentsAsync(Factory.GetString(), Factory.GetString());

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetDocuments_ServiceReturnsNull_LogsError()
        {
            string apiKey = Factory.GetString();
            _messageHandler.SendAsyncReturns(null);
            await _service.GetDocumentsAsync(apiKey, Factory.GetString());

            _logMock.VerifyLog(Level.Error, $"Query failed. Service response was NULL or status code not 200. API key {apiKey}", Times.Once());
        }

        [Fact]
        public async Task GetDocuments_ServiceReturnsNull_ReturnsEmpty()
        {
            _messageHandler.SendAsyncReturns(null);
            MediaDocument[] result = await _service.GetDocumentsAsync(Factory.GetString(), Factory.GetString());

            Assert.Empty(result);
        }
    }
}