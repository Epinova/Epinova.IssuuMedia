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

        public async Task<MediaDocument[]> GetDocumentsAsync(string apiKey, string apiSecret, int pageSize = 10, int startIndex = 0)
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
                { "startIndex", startIndex.ToString() },
                { "pageSize", pageSize.ToString() },
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
                _log.Information($"Query returned no results. Page size {pageSize}, start index {startIndex}, API key {apiKey}");
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
