using System;

namespace Epinova.IssuuMedia
{
    public class MediaDocumentEmbed
    {
        /// <summary>
        /// Timestamp for when embed was created in W3C format.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Configuration identifier for HTML code. Tells the embed widget which document embed to display
        /// </summary>
        public string DataConfigId { get; set; }

        public MediaDocument Document { get; set; }

        /// <summary>
        /// Unique identifier of the document shown by the embed. If document is deleted or removed after embed was created the field will be omitted from response
        /// </summary>
        public string DocumentId { get; set; }

        /// <summary>
        /// Height in pixels of the widget when embedded in webpage
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Id number of embed
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The page in the document which will initially be displayed.
        /// </summary>
        public int ReaderStartPage { get; set; }

        /// <summary>
        /// Width in pixels of the widget when embedded in webpage
        /// </summary>
        public int Width { get; set; }
    }
}
