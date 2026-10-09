using HomeSeerAPI;
using Scheduler.Classes;

namespace HSPI_UdpSeer
{
    public interface IUdpListenerPlugin
    {
        void InitIO(string port);
        void ShutdownIO();
    }
    public class UdpListenerPlugin: IUdpListenerPlugin
    {
        public UdpListenerPlugin(ILogging log,IHSApplication hs, IAppCallbackAPI callback, IInisettings iniSettings)
        {
            
        }

        public void InitIO(string port)
        {
            _log.LogDebug($"Starting {PluginConstants.PluginName}");
            Utility.PluginStartDateTime = SystemTime.Now();

            _config = new MainConfig(_log, _hs, _callback, _iniSettings, this, _ecowittMediator);
            _config.RegisterConfigs();

            _doUpdateWithNextPropertySetToGivenVersion = _updater.CheckIfPropertySetShouldBeUpdated(_hsDeviceUtils);

            _updater.RunUpdatesIfOlderVersion();
        }

        public void ShutdownIO()
        {
            throw new System.NotImplementedException();
        }
    }
}