using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeSeerAPI;
using UdpSeer.Logging;

namespace UdpSeer.HsDevices
{
    public interface IUdpSeerDataUpdater
    {
        void UpdateDeviceData(UnitData unitData);

    }
    public class UdpSeerDataUpdater: IUdpSeerDataUpdater
    {
        public UdpSeerDataUpdater(IHSApplication hs, ILogging log)
        {
            
        }
    }
}
