using OpenCV.Net;

namespace Bonsai.Emergent
{
    /// <summary>
    /// Represents a data object containing a decoded video frame and the metadata object
    /// which contains additional information about the image.
    /// </summary>
    /// <param name="image">The decoded video frame.</param>
    /// <param name="metadata">
    /// The metadata object which contains additional information about the image.
    /// </param>
    public class EmergentDataFrame(IplImage image, ImageMetadata metadata)
    {
        /// <summary>
        /// Gets the decoded video frame.
        /// </summary>
        public IplImage Image { get; } = image;

        /// <summary>
        /// Gets the metadata object which contains additional information about the image.
        /// </summary>
        public ImageMetadata Metadata { get; } = metadata;
    }
}
