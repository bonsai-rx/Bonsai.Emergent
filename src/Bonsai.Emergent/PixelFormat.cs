using Emergent;

namespace Bonsai.Emergent
{
    /// <summary>
    /// Specifies the pixel format for image data acquired from Emergent Vision cameras.
    /// </summary>
    public enum PixelFormat
    {
        /// <summary>
        /// Monochrome 8-bits per pixel.
        /// </summary>
        Mono8 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_MONO8,

        /// <summary>
        /// Monochrome 10-bits per pixel.
        /// </summary>
        Mono10 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_MONO10,

        /// <summary>
        /// Monochrome 10-bits per pixel packed (no padding bits).
        /// </summary>
        Mono10Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_MONO10_PACKED,

        /// <summary>
        /// Monochrome 12-bits per pixel.
        /// </summary>
        Mono12 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_MONO12,

        /// <summary>
        /// Monochrome 12-bits per pixel packed (no padding bits).
        /// </summary>
        Mono12Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_MONO12_PACKED,

        /// <summary>
        /// Bayer green blue filter 8-bits per pixel.
        /// </summary>
        BayerGB8 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGB8,

        /// <summary>
        /// Bayer green blue filter 10-bits per pixel.
        /// </summary>
        BayerGB10 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGB10,

        /// <summary>
        /// Bayer green blue filter 10-bits per pixel packed (no padding bits).
        /// </summary>
        BayerGB10Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGB10_PACKED,

        /// <summary>
        /// Bayer green blue filter 12-bits per pixel.
        /// </summary>
        BayerGB12 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGB12,

        /// <summary>
        /// Bayer green blue filter 12-bits per pixel packed (no padding bits).
        /// </summary>
        BayerGB12Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGB12_PACKED,

        /// <summary>
        /// Bayer green red filter 8-bits per pixel.
        /// </summary>
        BayerGR8 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGR8,

        /// <summary>
        /// Bayer green red filter 10-bits per pixel.
        /// </summary>
        BayerGR10 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGR10,

        /// <summary>
        /// Bayer green red filter 10-bits per pixel packed (no padding bits).
        /// </summary>
        BayerGR10Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGR10_PACKED,

        /// <summary>
        /// Bayer green red filter 12-bits per pixel.
        /// </summary>
        BayerGR12 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGR12,

        /// <summary>
        /// Bayer green red filter 12-bits per pixel packed (no padding bits).
        /// </summary>
        BayerGR12Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGR12_PACKED,

        /// <summary>
        /// Bayer red green filter 8-bits per pixel.
        /// </summary>
        BayerRG8 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYRG8,

        /// <summary>
        /// Bayer red green filter 10-bits per pixel.
        /// </summary>
        BayerRG10 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYRG10,

        /// <summary>
        /// Bayer red green filter 10-bits per pixel packed (no padding bits).
        /// </summary>
        BayerRG10Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYRG10_PACKED,

        /// <summary>
        /// Bayer red green filter 12-bits per pixel.
        /// </summary>
        BayerRG12 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYRG12,

        /// <summary>
        /// Bayer red green filter 12-bits per pixel packed (no padding bits).
        /// </summary>
        BayerRG12Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYRG12_PACKED,

        /// <summary>
        /// Bayer blue green filter 8-bits per pixel.
        /// </summary>
        BayerBG8 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYBG8,

        /// <summary>
        /// Bayer blue green filter 10-bits per pixel.
        /// </summary>
        BayerBG10 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYBG10,

        /// <summary>
        /// Bayer blue green filter 10-bits per pixel packed (no padding bits).
        /// </summary>
        BayerBG10Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYBG10_PACKED,

        /// <summary>
        /// Bayer blue green filter 12-bits per pixel.
        /// </summary>
        BayerBG12 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYBG12,

        /// <summary>
        /// Bayer blue green filter 12-bits per pixel packed (no padding bits).
        /// </summary>
        BayerBG12Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYBG12_PACKED,

        /// <summary>
        /// Color red, green, blue 8-bit channels per pixel.
        /// </summary>
        RGB8 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_RGB8,

        /// <summary>
        /// Color red, green, blue 10-bit channels per pixel.
        /// </summary>
        RGB10 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_RGB10,

        /// <summary>
        /// Color red, green, blue 12-bit channels per pixel.
        /// </summary>
        RGB12 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_RGB12,

        /// <summary>
        /// Color blue, green, red 8-bit channels per pixel.
        /// </summary>
        BGR8 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BGR8,

        /// <summary>
        /// Color blue, green, red 10-bit channels per pixel.
        /// </summary>
        BGR10 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BGR10,

        /// <summary>
        /// Color blue, green, red 12-bit channels per pixel.
        /// </summary>
        BGR12 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BGR12,

        /// <summary>
        /// YUV 411 pixel format packed (no padding bits).
        /// </summary>
        /// <remarks>
        /// For every four pixels, the Y component is fully sampled for all, but the U and V
        /// components are sampled once and shared across these four pixels.
        /// </remarks>
        YUV411Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_YUV411_PACKED,

        /// <summary>
        /// YUV 422 pixel format packed (no padding bits).
        /// </summary>
        /// <remarks>
        /// For every two pixels, the Y component is fully sampled for both, but the U and V
        /// components are sampled once and shared across these two pixels.
        /// </remarks>
        YUV422Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_YUV422_PACKED,

        /// <summary>
        /// YUV 444 pixel format packed (no padding bits). The Y, U, and V components are fully
        /// sampled for every pixel.
        /// </summary>
        YUV444Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_YUV444_PACKED
    }
}
