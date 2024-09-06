using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergent;

namespace Bonsai.Emergent
{
    public class EmergentCapture : Source<int>
    {
        [TypeConverter(typeof(CameraIdConverter))]
        public string CameraId { get; set; }

        public override IObservable<int> Generate()
        {
            throw new NotImplementedException();
        }
    }
}
