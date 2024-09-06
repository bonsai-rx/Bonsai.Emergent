using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergent;

namespace Bonsai.Emergent
{
    public class EmergentCapture : Source<CEmergentFrameDotNet>
    {
        public int Index { get; set; }

        //[TypeConverter(typeof(CameraIdConverter))]
        //public string CameraId { get; set; }

        public override IObservable<CEmergentFrameDotNet> Generate()
        {
            return Observable.Create<CEmergentFrameDotNet>((observer, cancellationToken) =>
            {
                return Task.Factory.StartNew(async () => {
                    // Configuration
                    List<CGigEVisionDeviceInfoDotNet> deviceInfoList = new List<CGigEVisionDeviceInfoDotNet>();
                    CEmergentCameraDotNet.ListDevices(deviceInfoList);

                    var camera = new CEmergentCameraDotNet();

                    try
                    {
                        Console.WriteLine("Attempting open camera");
                        camera.Open(deviceInfoList[Index]);
                        Console.WriteLine("Opened camera");
                    }
                    catch (Exception ex) { observer.OnError(ex); }

                    var frame = new CEmergentFrameDotNet();

                    // Acqusition loop
                    try
                    {
                        using (var cancellation = cancellationToken.Register(() => { camera.ExecuteCommand("AcquisitionStop"); }))
                        {
                            while (!cancellationToken.IsCancellationRequested)
                            {
                                // get images etc.

                                // placeholder
                                var error = camera.WaitforFrame(frame, 1);
                                Console.WriteLine(error);
                                observer.OnNext(frame);
                            };
                        }
                    }
                    catch (Exception ex) { observer.OnError(ex); }
                    finally
                    {
                        camera.CloseStream();
                        camera.Close();
                    }
                },
                cancellationToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
            });
        }
    }
}
