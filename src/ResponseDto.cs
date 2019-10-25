using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace Epinova.IssuuMedia
{
    internal class ResponseDto<TContent>
    {
        [JsonProperty("_content")]
        public ResponseContentDto<TContent> content { get; set; }

        public string stat { get; set; }
    }
}
