using Emergent;

namespace Bonsai.Emergent
{
    /// <summary>
    /// Represents additional information about an image decoded from an Emergent Vision camera.
    /// </summary>
    public class ImageMetadata
    {
        internal ImageMetadata(CEmergentFrameDotNet frame)
        {
            NanoSeconds = frame.NanoSeconds;
            Timestamp = frame.Timestamp;
            FrameId = frame.FrameId;
            PixelFormat = (PixelFormat)frame.PixelFormat;
        }

        /// <summary>
        /// Gets the precise timestamp in nanoseconds for frames using Myri sync NIC and IRIGB.
        /// </summary>
        public ulong NanoSeconds { get; }

        /// <summary>
        /// Gets the GigE Vision timestamp. If the camera is in precision time protocol (PTP) mode,
        /// this is the PTP clock of the camera.
        /// </summary>
        public ulong Timestamp { get; }

        /// <summary>
        /// Gets the 16-bit block identifier from the GigE Vision Streaming Protocol.
        /// </summary>
        public ushort FrameId { get; }

        /// <summary>
        /// Gets the pixel format of the decoded image data.
        /// </summary>
        public PixelFormat PixelFormat { get; }
    }
}
