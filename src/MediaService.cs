using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Epinova.Infrastructure;
using Epinova.Infrastructure.Logging;
using EPiServer.Logging;

namespace Epinova.IssuuMedia
{
    public class MediaService : RestServiceBase, IMediaService
    {
        internal static HttpClient Client = new HttpClient { BaseAddress = new Uri("https://api.issuu.com/1_0/") };
        private readonly ILogger _log;
        private readonly IMapper _mapper;

        public MediaService(ILogger log, IMapper mapper) : base(log)
        {
            _log = log;
            _mapper = mapper;
        }

        public async Task<MediaDocumentEmbed[]> GetDocumentEmbedsAsync(string apiKey, string apiSecret)
        {
            if (String.IsNullOrWhiteSpace(apiKey) || String.IsNullOrWhiteSpace(apiSecret))
            {
                _log.Warning("Missing API key and/or secret");
                return new MediaDocumentEmbed[0];
            }

            MediaDocument[] documents = await GetDocumentsAsync(apiKey, apiSecret);
            if (!documents.Any())
            {
                _log.Information($"No documents found using API key {apiKey}");
                return new MediaDocumentEmbed[0];
            }

            var result = new List<MediaDocumentEmbed>();

            foreach (MediaDocument document in documents)
            {
                var parameters = new SortedDictionary<string, string>
                {
                    { "apiKey", apiKey },
                    { "action", "issuu.document_embeds.list" },
                    { "format", "json" },
                    { "documentId", document.DocumentId },
                    { "embedSortBy", nameof(MediaDocumentEmbedDto.created) },
                    { "resultOrder", "desc" },
                    { "pageSize", "10" },
                };
                parameters.Add("signature", CalculateMd5Hash(apiSecret, parameters));

                string url = $"?{BuildQueryString(parameters)}";

                HttpResponseMessage responseMessage = await CallAsync(() => Client.GetAsync(url), true);

                if (responseMessage == null || !responseMessage.IsSuccessStatusCode)
                {
                    _log.Error($"Query failed. Service response was NULL or status code not 200. API key {apiKey}");
                    continue;
                }

                ResponseRootDto<ResponseDocumentEmbedContentDto> dto = await ParseJsonAsync<ResponseRootDto<ResponseDocumentEmbedContentDto>>(responseMessage);

                if (dto.HasError || dto.rsp == null || !dto.rsp.stat.Equals("ok", StringComparison.OrdinalIgnoreCase))
                {
                    _log.Error(new { message = "Query failed.", dto, apiKey });
                    continue;
                }

                if (dto.rsp?.content?.result?.content == null || !dto.rsp.content.result.content.Any())
                {
                    _log.Warning(new { message = "Query returned no result.", dto, apiKey });
                    continue;
                }

                var embeds = _mapper.Map<List<MediaDocumentEmbed>>(dto.rsp.content.result.content.Select(c => c.documentEmbed));
                embeds.ForEach(embed => embed.Document = document);

                result.AddRange(embeds);
            }

            return result.ToArray();
        }

        public async Task<MediaDocument[]> GetDocumentsAsync(string apiKey, string apiSecret)
        {
            if (String.IsNullOrWhiteSpace(apiKey) || String.IsNullOrWhiteSpace(apiSecret))
            {
                _log.Warning("Missing API key and/or secret");
                return new MediaDocument[0];
            }

            var parameters = new SortedDictionary<string, string>
            {
                { "apiKey", apiKey },
                { "action", "issuu.documents.list" },
                { "documentStates", "A" },
                { "access", "public" },
                { "format", "json" },
                { "documentSortBy", nameof(MediaDocumentDto.publishDate) },
                { "resultOrder", "desc" },
                { "pageSize", "10" },
                { "responseParams", MediaDocumentDto.GetResponseParameters() },
            };
            parameters.Add("signature", CalculateMd5Hash(apiSecret, parameters));

            string url = $"?{BuildQueryString(parameters)}";

            HttpResponseMessage responseMessage = await CallAsync(() => Client.GetAsync(url), true);

            if (responseMessage == null || !responseMessage.IsSuccessStatusCode)
            {
                _log.Error($"Query failed. Service response was NULL or status code not 200. API key {apiKey}");
                return new MediaDocument[0];
            }

            ResponseRootDto<ResponseDocumentContentDto> dto = await ParseJsonAsync<ResponseRootDto<ResponseDocumentContentDto>>(responseMessage);

            if (dto.HasError || dto.rsp == null || !dto.rsp.stat.Equals("ok", StringComparison.OrdinalIgnoreCase))
            {
                _log.Error(new { message = "Query failed.", dto, apiKey });
                return new MediaDocument[0];
            }

            if (dto.rsp?.content?.result?.content == null || !dto.rsp.content.result.content.Any())
            {
                _log.Warning(new { message = "Query returned no result.", dto, apiKey });
                return new MediaDocument[0];
            }

            return _mapper.Map<MediaDocument[]>(dto.rsp.content.result.content.Select(c => c.document));
        }

        private static string CalculateMd5Hash(string apiSecret, SortedDictionary<string, string> input)
        {
            return CalculateMd5Hash($"{apiSecret}{String.Join("", input.Select(d => $"{d.Key}{d.Value}"))}");
        }

        private static string CalculateMd5Hash(string input)
        {
            if (String.IsNullOrEmpty(input))
                return null;
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (byte t in hash)
                sb.Append(t.ToString("x2"));

            return sb.ToString();
        }
    }
}