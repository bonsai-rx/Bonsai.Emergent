using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Emergent;

namespace Bonsai.Emergent
{
    internal class SerialNumberConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            var deviceInfoList = new List<CGigEVisionDeviceInfoDotNet>();
            CEmergentCameraDotNet.ListDevices(deviceInfoList);

            return new StandardValuesCollection(deviceInfoList.Select(x => x.SerialNumber).ToList());
        }
    }
}
