using System.Threading.Tasks;

namespace Epinova.IssuuMedia
{
    public interface IMediaService
    {
        /// <summary>
        /// List all documents belonging to a user profile
        /// </summary>
        /// <param name="apiKey">Application key for the account</param>
        /// <param name="apiSecret">Secret for signing the request towards Issuu</param>
        /// <param name="pageSize">Maximum number of documents to be returned. Value must be between 0 - 30. Default is 10</param>
        /// <param name="startIndex">Zero based index to start pagination from</param>
        Task<MediaDocument[]> GetDocumentsAsync(string apiKey, string apiSecret, int pageSize = 10, int startIndex = 0);
    }
}