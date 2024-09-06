using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergent;

namespace Bonsai.Emergent
{
    public class CameraIdConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<CGigEVisionDeviceInfoDotNet> deviceInfoList = new List<CGigEVisionDeviceInfoDotNet>();
            CEmergentCameraDotNet.ListDevices(deviceInfoList);

            return new StandardValuesCollection(deviceInfoList.Select(x => x.SerialNumber).ToList());
        }
    }
}
