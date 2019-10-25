using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace Epinova.IssuuMedia
{
    internal class ResponseResultDto<TContent>
    {
        [JsonProperty("_content")]
        public TContent[] content { get; set; }

        public bool more { get; set; }
        public int pageSize { get; set; }
        public int startIndex { get; set; }
        public int totalCount { get; set; }
    }
}
