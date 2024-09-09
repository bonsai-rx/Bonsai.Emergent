using Emergent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Emergent
{
    public static class EmergentUtils
    {
        public static Dictionary<string, CEmergentFrameDotNet.EPixelFormat> dictPixelFormat = new Dictionary<string, CEmergentFrameDotNet.EPixelFormat> { 
            {"Mono8", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_MONO8},
            {"Mono10", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_MONO10},
            {"Mono10Packed", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_MONO10_PACKED},
            {"Mono12", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_MONO12},
            {"Mono12Packed", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_MONO12_PACKED},
            {"BayerGB8", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGB8},
            {"BayerGB10", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGB10},
            {"BayerGB10Packed", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGB10_PACKED},
            {"BayerGB12", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGB12},
            {"BayerGB12Packed", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGB12_PACKED},
            {"BayerGR8", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGR8},
            {"BayerGR10", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGR10},
            {"BayerGR10Packed", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGR10_PACKED},
            {"BayerGR12", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGR12},
            {"BayerGR12Packed", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYGR12_PACKED},
            {"BayerRG8", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYRG8},
            {"BayerRG10", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYRG10},
            {"BayerRG10Packed", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYRG10_PACKED},
            {"BayerRG12", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYRG12},
            {"BayerRG12Packed", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYRG12_PACKED},
            {"BayerBG8", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYBG8},
            {"BayerBG10", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYBG10},
            {"BayerBG10Packed", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYBG10_PACKED},
            {"BayerBG12", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYBG12},
            {"BayerBG12Packed", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BAYBG12_PACKED},
            {"RGB8Packed", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_RGB8},
            {"RGB10Packed", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_RGB10},
            {"RGB12Packed", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_RGB12},
            {"BGR8Packed", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BGR8},
            {"BGR10Packed", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BGR10},
            {"BGR12Packed", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_BGR12},
            {"YUV411Packed", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_YUV411_PACKED},
            {"YUV422Packed", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_YUV422_PACKED},
            {"YUV444Packed", CEmergentFrameDotNet.EPixelFormat.EGVSP_PIX_YUV444_PACKED}
        };

        public static string GetPixelEnumString(CEmergentFrameDotNet.EPixelFormat enumValue)
        {
            return dictPixelFormat.Keys.Where(x => dictPixelFormat[x] == enumValue).FirstOrDefault();
        }
    }
}
