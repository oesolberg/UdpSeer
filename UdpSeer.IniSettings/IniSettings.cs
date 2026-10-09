using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeSeerAPI;
using UdpSeer.Common;

namespace UdpSeer.IniSettings
{
    public interface IIniSettings
    {
        void LoadSettingsFromIniFile();
        LogLevel LogLevel { get; set; }
    }
    public class IniSettings: IIniSettings
    {
        private readonly string ConfigSection = "Config";
        public const string LogLevelKey = "Loglevel";
        public string PortKey = "PortNumber";
        public string DeviceConfigSection = "Devices";

        private readonly IHSApplication _hs;
        private LogLevel _logLevel;
        private Dictionary<string, int> _unitNamesAndDeviceIds;
        private int _port;

        public IniSettings(IHSApplication hs)
        {
            _hs = hs;
        }
        public void LoadSettingsFromIniFile()
        {
                _logLevel = GetLogLevel();
                _port = GetPortNumber();
                GetUnitNamesToDeviceIds();

        }

        private void GetUnitNamesToDeviceIds()
        {
            _unitNamesAndDeviceIds = new Dictionary<string, int>();
            var unitNamesAndDeviceIdsInIni = _hs.GetINISectionEx(DeviceConfigSection,  PluginConstants.Inifile).ToList();
            CreateUnitNamesAndDeviceIds(unitNamesAndDeviceIdsInIni);
        }

        private void CreateUnitNamesAndDeviceIds(List<string> unitNamesAndDeviceIdsInIni)
        {
            foreach (var iniLine in unitNamesAndDeviceIdsInIni)
            {
                var splitArray = iniLine.Split(new char[] { '=' }, 2);
                if (splitArray.Length > 1 && !string.IsNullOrWhiteSpace(splitArray[splitArray.Length - 1]))
                {
                    var unitName = splitArray[1];
                    var unitIdAsString = splitArray[1];
                    if(int.TryParse(unitIdAsString,out int unitId))
                        _unitNamesAndDeviceIds.Add(unitName,unitId);
                    else
                    {
                        throw new Exception($"Unrecognised device id in string {iniLine}");
                    }
                }
            }
        }


        public LogLevel LogLevel
        {
            get => _logLevel;
            set
            {
                _logLevel = value;
                SaveLogLevel();
            }
        }

        public int Port
        {
            get => _port;
            set
            {
                _port = value;
                SavePortNumber();
            }
        }

        private void SavePortNumber()
        {
            throw new NotImplementedException();
        }


        private int GetPortNumber()
        {
            var portAsString = _hs.GetINISetting(ConfigSection, PortKey, "49999", PluginConstants.Inifile);
            int portNumber;
            if (!int.TryParse(portAsString, out portNumber))
            {
                portNumber = 49999;
            }

            return portNumber;
        }

        

        private LogLevel GetLogLevel()
        {
            var debugLevelAsString = _hs.GetINISetting(ConfigSection, LogLevelKey, "NONE", PluginConstants.Inifile);
            LogLevel logLevelToReturn;
            if (!Enum.TryParse(debugLevelAsString, true, out logLevelToReturn))
            {
                logLevelToReturn = LogLevel.Normal;
            }
            return logLevelToReturn;
        }

        private void SaveLogLevel()
        {

            var loglevelToSave = Enum.GetName(typeof(LogLevel), _logLevel);
            _hs.SaveINISetting(ConfigSection, LogLevelKey, loglevelToSave, PluginConstants.Inifile);
        }
    }
}
