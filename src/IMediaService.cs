using System.Threading.Tasks;

namespace Epinova.IssuuMedia
{
    public interface IMediaService
    {
        /// <summary>
        /// List all document embeds for the account
        /// </summary>
        /// <param name="apiKey">Application key for the account</param>
        /// <param name="apiSecret">Secret for signing the request towards Issuu</param>
        Task<MediaDocumentEmbed[]> GetDocumentEmbedsAsync(string apiKey, string apiSecret);

        /// <summary>
        /// List all documents belonging to a user profile
        /// </summary>
        /// <param name="apiKey">Application key for the account</param>
        /// <param name="apiSecret">Secret for signing the request towards Issuu</param>
        Task<MediaDocument[]> GetDocumentsAsync(string apiKey, string apiSecret);
    }
}