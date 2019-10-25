// ReSharper disable InconsistentNaming

namespace Epinova.IssuuMedia
{
    internal class ResponseContentDto<TContent>
    {
        public ResponseErrorDto error { get; set; }
        public ResponseResultDto<TContent> result { get; set; }
    }
}
