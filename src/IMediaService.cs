using System.Threading.Tasks;

namespace Epinova.IssuuMedia
{
    public interface IMediaService
    {
        Task<MediaDocumentEmbed[]> GetDocumentEmbedsAsync(string apiKey, string apiSecret);
        Task<MediaDocument[]> GetDocumentsAsync(string apiKey, string apiSecret);
    }
}