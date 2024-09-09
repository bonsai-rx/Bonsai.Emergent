using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Reactive;
using Emergent;

namespace Bonsai.Emergent
{
    public class EmergentCapture : Source<CEmergentFrameDotNet>
    {
        //public int Index { get; set; }

        [TypeConverter(typeof(CameraIdConverter))]
        public string SerialNumber { get; set; }

        public CEmergentFrameDotNet.EPixelFormat PixelFormat { get; set; } = CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_MONO8;

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

        public override IObservable<CEmergentFrameDotNet> Generate()
        {
            return Observable.Create<CEmergentFrameDotNet>((observer, cancellationToken) =>
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
                    camera.SetEnumByString("PixelFormat", EmergentUtils.GetPixelEnumString(PixelFormat));
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
                                buffers[i].PixelFormat = PixelFormat;
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

                            var result = camera.WaitforFrame(frameTemp, -1);
                            Console.WriteLine(result);

                            while (!cancellationToken.IsCancellationRequested)
                            {
                                //var result = camera.WaitforFrame(frameTemp, -1);
                                //if (result == EmergentErrorsDotNet.EVT_SUCCESS)
                                //{
                                //    observer.OnNext(frameTemp);
                                //}
                                //camera.QueueFrameBuffer(frameTemp);
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
}
