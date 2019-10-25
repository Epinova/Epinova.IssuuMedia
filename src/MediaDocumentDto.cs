using System;

// ReSharper disable InconsistentNaming

namespace Epinova.IssuuMedia
{
    internal class MediaDocumentDto
    {
        public string documentId { get; set; }
        public string name { get; set; }
        public string publicationId { get; set; }
        public DateTime publishDate { get; set; }
        public string revisionId { get; set; }
        public string title { get; set; }
        public string username { get; set; }

        public static string GetResponseParameters()
        {
            return String.Join(",", nameof(documentId), nameof(name), nameof(publicationId), nameof(revisionId), nameof(title), nameof(username), nameof(publishDate));
        }
    }
}
