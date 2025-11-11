using Emergent;
using OpenCV.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Bonsai.Emergent
{
    /// <summary>
    /// Represents an operator that generates a sequence of images acquired from
    /// the specified Emergent Vision camera.
    /// </summary>
    [Description("Generates a sequence of images acquired from the specified Emergent Vision camera.")]
    public class EmergentCapture : Source<EmergentDataFrame>
    {
        /// <summary>
        /// Gets or sets the serial number of the camera from which to acquire images.
        /// </summary>
        [TypeConverter(typeof(SerialNumberConverter))]
        [Description("The serial number of the camera from which to acquire images.")]
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the pixel format for acquired images.
        /// </summary>
        [Description("The pixel format for acquired images.")]
        public PixelFormat PixelFormat { get; set; } = PixelFormat.Mono8;

        /// <summary>
        /// Gets or sets the maximum number of preallocated frame buffers.
        /// </summary>
        [Description("The maximum number of preallocated frame buffers.")]
        public int MaxBufferLength { get; set; } = 30;

        /// <summary>
        /// Generates an observable sequence of decoded video frame data and metadata
        /// acquired from the specified Emergent Vision camera.
        /// </summary>
        /// <returns>
        /// A sequence of <see cref="EmergentDataFrame"/> objects representing each
        /// decoded video frame data and metadata acquired from the camera.
        /// </returns>
        public override IObservable<EmergentDataFrame> Generate()
        {
            return Observable.Create<EmergentDataFrame>((observer, cancellationToken) =>
            {
                var pixelFormat = PixelFormat;
                var serialNumber = SerialNumber;
                if (string.IsNullOrEmpty(serialNumber))
                {
                    throw new InvalidOperationException("The serial number of the camera must be specified.");
                }

                var maxBufferLength = MaxBufferLength;
                if (maxBufferLength <= 0)
                {
                    throw new InvalidOperationException("At least one preallocated frame buffer must be used.");
                }

                return Task.Factory.StartNew(() =>
                {
                    CEmergentCameraDotNet camera = null;
                    CEmergentFrameDotNet[] frameBuffers = null;
                    try
                    {
                        var deviceInfoList = new List<CGigEVisionDeviceInfoDotNet>();
                        CEmergentCameraDotNet.ListDevices(deviceInfoList);

                        var cameraInfo = deviceInfoList.FirstOrDefault(device => device.SerialNumber == serialNumber)
                            ?? throw new InvalidOperationException("No device with the specified serial number was found.");

                        camera = new CEmergentCameraDotNet();
                        camera.Open(cameraInfo);

                        // Configuration - camera settings
                        camera.SetEnumByString("PixelFormat", Enum.GetName(typeof(PixelFormat), pixelFormat));
                        var pixelFormatS = camera.GetEnum("PixelFormat");
                        pixelFormat = (PixelFormat)Enum.Parse(typeof(PixelFormat), pixelFormatS);
                        var wMax = camera.GetUInt32Max("Width");
                        var hMax = camera.GetUInt32Max("Height");

                        camera.OpenStream();
                        var allocatedFrames = new List<CEmergentFrameDotNet>(maxBufferLength);
                        for (int i = 0; i < maxBufferLength; i++)
                        {
                            var frame = new CEmergentFrameDotNet
                            {
                                PixelFormat = (CEmergentFrameDotNet.EPixelFormat)pixelFormat,
                                Width = wMax,
                                Height = hMax
                            };

                            if (camera.AllocateFrameBuffer(frame, CEmergentFrameDotNet.EFRAME_BUFFER_TYPE.EEVT_FRAME_BUFFER_ZERO_COPY) != EmergentErrorsDotNet.EVT_SUCCESS)
                                break;

                            if (camera.QueueFrameBuffer(frame) != EmergentErrorsDotNet.EVT_SUCCESS)
                                break;

                            allocatedFrames.Add(frame);
                        }

                        frameBuffers = allocatedFrames.ToArray();
                        camera.ExecuteCommand("AcquisitionStart");

                        var frameTemp = new CEmergentFrameDotNet();
                        while (!cancellationToken.IsCancellationRequested)
                        {
                            var result = camera.WaitforFrame(frameTemp, -1);
                            if (result == EmergentErrorsDotNet.EVT_SUCCESS)
                            {
                                // Conversion
                                var image = new IplImage(new Size((int)wMax, (int)hMax), IplDepth.U8, 1, frameTemp.DataPtr);
                                var metadata = new ImageMetadata(frameTemp);
                                observer.OnNext(new EmergentDataFrame(image, metadata));
                            }
                            camera.QueueFrameBuffer(frameTemp);
                        }
                    }
                    catch (Exception ex)
                    {
                        observer.OnError(ex);
                        throw;
                    }
                    finally
                    {
                        if (camera is not null)
                        {
                            camera.ExecuteCommand("AcquisitionStop");
                            if (frameBuffers is not null)
                            {
                                for (int i = 0; i < frameBuffers.Length; i++)
                                {
                                    camera.ReleaseFrameBuffer(frameBuffers[i]);
                                }
                            }

                            camera.CloseStream();
                            camera.Close();
                        }
                    }
                },
                cancellationToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
            });
        }
    }
}
