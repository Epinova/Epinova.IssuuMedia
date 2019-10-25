// ReSharper disable InconsistentNaming

namespace Epinova.IssuuMedia
{
    internal class ResponseRootDto<TContent> : ResponseDtoBase
    {
        public ResponseDto<TContent> rsp { get; set; }
    }
}
