using System;

namespace Epinova.IssuuMedia
{
    public class MediaDocument
    {
        /// <summary>
        /// Concatenated value of revisionId and publicationId (with a hyphen in between).
        /// </summary>
        public string DocumentId { get; set; }

        /// <summary>
        /// Name of document Combined with username this defines documents location on Issuu: http://issuu.com/{username}/docs/{name}
        /// </summary>
        public string DocumentName { get; set; }

        /// <summary>
        /// Unique assigned id of the publication formatted as 32 hex digits. The id remains constant for all revisions of a publication.
        /// </summary>
        public string PublicationId { get; set; }

        /// <summary>
        /// Timestamp for when this document was published
        /// </summary>
        public DateTime PublishedOn { get; set; }

        /// <summary>
        /// Identifier of the current revision of a publication. When a new revision is uploaded this id will change while the publicationId remains constant.
        /// A revisionId is only unique within a given publication.
        /// </summary>
        public string RevisionId { get; set; }

        /// <summary>
        /// Title of the document
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Owner of document
        /// </summary>
        public string UserName { get; set; }
    }
}