using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HomeSeerAPI;
using UdpSeer.IniSettings;
using UdpSeer.Logging;

namespace UdpSeer.MainPlugin
{
    public class UdpSeerPlugin
    {
        private readonly ILogging _log;
        private readonly IHSApplication _hs;
        private readonly IAppCallbackAPI _callback;
        private readonly IIniSettings _iniSettings;
        //private IMainConfig _config;
        private Dictionary<string, UnitData> _ecowittDataDictionary;
        private bool _disposed;
        //private IEcowittMediator _ecowittMediator;
        private IUnitDataHandler _unitDataHandler;
        private ConcurrentDictionary<string, UnitData> _unitAndConversionDictionary;
        private IDeviceHandler _deviceHandler;
        private readonly string _form = "form";
        private List<string> _keysToUpdate;
        private Thread _dataUpdateThread;
        private DateTime _threadLastRunTime = DateTime.MinValue;
        //private IPluginUpdater _updater;
        private IHomeSeerDeviceUtils _hsDeviceUtils;
        //private int _doUpdateWithNextPropertySetToGivenVersion = -1;
        //private int _runNumber;

        public UdpSeerPlugin(ILogging log, IHSApplication hs, IAppCallbackAPI callback, IIniSettings iniSettings)
        {
            _log = log;
            _hs = hs;
            _callback = callback;
            _iniSettings = iniSettings;

            _ecowittDataDictionary = new Dictionary<string, UnitData>();
            _unitAndConversionDictionary = GetConversionsFromIni();

            _ecowittMediator = new EcowittMediator();
            _ecowittMediator.AddHandler(this);

            _iniSettings.SetMediator(_ecowittMediator);

            _unitDataHandler = new UnitDataHandler(_log);

            _hsDeviceUtils = new HomeSeerDeviceUtils(_hs, _log);

            _deviceHandler = new DeviceHandler(_ecowittMediator, _hs, _hsDeviceUtils, _log, _iniSettings);
            _updater = new PluginUpdater(_hsDeviceUtils, _hs, _log);
        }
    }
}
