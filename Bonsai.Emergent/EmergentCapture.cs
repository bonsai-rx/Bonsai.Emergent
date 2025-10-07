using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Reactive;
using Emergent;
using OpenCV.Net;

namespace Bonsai.Emergent
{
    public class EmergentCapture : Source<IplImage>
    {
        //public int Index { get; set; }

        [TypeConverter(typeof(SerialNumberConverter))]
        public string SerialNumber { get; set; }

        public PixelFormat PixelFormat { get; set; } = PixelFormat.Mono8;

        void CloseCameraStream(CEmergentCameraDotNet camera)
        {
            camera.ExecuteCommand("AcquisitionStop");

            camera.CloseStream();
            camera.Close();
        }

        void CloseCameraStream(CEmergentCameraDotNet camera, CEmergentFrameDotNet[] buffer)
        {
            camera.ExecuteCommand("AcquisitionStop");

            for (int i = 0; i < buffer.Length; i++)
            {
                camera.ReleaseFrameBuffer(buffer[i]);
            }

            camera.CloseStream();
            camera.Close();
        }

        public override IObservable<IplImage> Generate()
        {
            return Observable.Create<IplImage>((observer, cancellationToken) =>
            {
                return Task.Factory.StartNew(() => {
                    // Configuration - open camera
                    List<CGigEVisionDeviceInfoDotNet> deviceInfoList = new List<CGigEVisionDeviceInfoDotNet>();
                    CEmergentCameraDotNet.ListDevices(deviceInfoList);

                    var camera = new CEmergentCameraDotNet();

                    try
                    {
                        Console.WriteLine("Attempting open camera");
                        camera.Open(deviceInfoList.Where(x => x.SerialNumber == SerialNumber).FirstOrDefault());
                        Console.WriteLine("Opened camera");
                    }
                    catch (Exception ex) { observer.OnError(ex); }

                    // Configuration - camera settings
                    camera.SetEnumByString("PixelFormat", Enum.GetName(typeof(PixelFormat), PixelFormat));
                    string pixelFormatS = camera.GetEnum("PixelFormat");
                    Console.WriteLine("Pixel format: {0}", pixelFormatS);

                    var wMax = camera.GetUInt32Max("Width");
                    var hMax = camera.GetUInt32Max("Height");
                    Console.WriteLine("Resolution: {0} x {1}", wMax, hMax);

                    var fMax = camera.GetUInt32Max("FrameRate");
                    Console.WriteLine("FrameRate Max: \t\t{0}", fMax);

                    const int allocatedFrameCount = 30;
                    var buffers = new CEmergentFrameDotNet[allocatedFrameCount];

                    // Acqusition loop
                    try
                    {
                        using (var cancellation = cancellationToken.Register(() => { CloseCameraStream(camera); }))
                        {
                            camera.OpenStream();

                            // set up acquisition buffer
                            for (int i = 0; i < allocatedFrameCount; i++)
                            {
                                buffers[i] = new CEmergentFrameDotNet();
                                buffers[i].PixelFormat = (CEmergentFrameDotNet.EPixelFormat)PixelFormat;
                                buffers[i].Width = wMax;
                                buffers[i].Height = hMax;

                                if (camera.AllocateFrameBuffer(buffers[i], CEmergentFrameDotNet.EFRAME_BUFFER_TYPE.EEVT_FRAME_BUFFER_ZERO_COPY) != EmergentErrorsDotNet.EVT_SUCCESS)
                                {
                                    break;
                                }
                                if (camera.QueueFrameBuffer(buffers[i]) != EmergentErrorsDotNet.EVT_SUCCESS)
                                {
                                    break;
                                }
                            }

                            camera.ExecuteCommand("AcquisitionStart");

                            var frameTemp = new CEmergentFrameDotNet();

                            while (!cancellationToken.IsCancellationRequested)
                            {
                                var result = camera.WaitforFrame(frameTemp, -1);
                                if (result == EmergentErrorsDotNet.EVT_SUCCESS)
                                {
                                    // Conversion
                                    var output = new IplImage(new Size((int)wMax, (int)hMax), IplDepth.U8, 1, frameTemp.DataPtr);

                                    observer.OnNext(output);
                                }
                                Console.WriteLine(result);
                                camera.QueueFrameBuffer(frameTemp);
                            };
                        }
                    }
                    catch (Exception ex) { observer.OnError(ex); }
                    finally
                    {
                        CloseCameraStream(camera, buffers);
                    }
                },
                cancellationToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
            });
        }
    }
    public enum PixelFormat
    {
        Mono8 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_MONO8,
        Mono10 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_MONO10,
        Mono10Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_MONO10_PACKED,
        Mono12 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_MONO12,
        Mono12Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_MONO12_PACKED,
        BayerGB8 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGB8,
        BayerGB10 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGB10,
        BayerGB10Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGB10_PACKED,
        BayerGB12 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGB12,
        BayerGB12Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGB12_PACKED,
        BayerGR8 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGR8,
        BayerGR10 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGR10,
        BayerGR10Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGR10_PACKED,
        BayerGR12 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGR12,
        BayerGR12Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGR12_PACKED,
        BayerRG8 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYRG8,
        BayerRG10 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYRG10,
        BayerRG10Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYRG10_PACKED,
        BayerRG12 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYRG12,
        BayerRG12Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYRG12_PACKED,
        BayerBG8 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYBG8,
        BayerBG10 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYBG10,
        BayerBG10Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYBG10_PACKED,
        BayerBG12 = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYBG12,
        BayerBG12Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYBG12_PACKED,
        RGB8Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_RGB8,
        RGB10Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_RGB10,
        RGB12Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_RGB12,
        BGR8Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BGR8,
        BGR10Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BGR10,
        BGR12Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BGR12,
        YUV411Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_YUV411_PACKED,
        YUV422Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_YUV422_PACKED,
        YUV444Packed = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_YUV444_PACKED
    }
}
