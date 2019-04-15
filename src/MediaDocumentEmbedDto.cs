using System;

// ReSharper disable InconsistentNaming

namespace Epinova.IssuuMedia
{
    internal class MediaDocumentEmbedDto
    {
        public DateTime created { get; set; }

        public string dataConfigId { get; set; }

        public string documentId { get; set; }

        public int height { get; set; }

        public int id { get; set; }

        public int readerStartPage { get; set; }

        public int width { get; set; }
    }
}