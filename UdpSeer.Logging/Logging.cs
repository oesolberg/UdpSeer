using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HomeSeerAPI;
using UdpSeer.Common;
using UdpSeer.IniSettings;
using Serilog;
using Serilog.Core;

namespace UdpSeer.Logging
{
    public interface ILogging
    {
        void LogToHsNoMatterSettings(string logMessage);
        void Log(string logMessage);
        void Log(string logMessage, LogLevel logLevel);
        void LogDebug(string logMessage);
        LogLevel LogLevel { get; }
        bool IsLoggingToFile { get; }
        bool IsLoggingDebug { get; }
        void LogException(Exception ex);
        void LogError(string logMessage);
        void LogWarning(string logMessage);

        void LogIfLogToFileEnabled(string logMessage, LogLevel logLevel = LogLevel.Debug);
        void IniSettingHasChanged(object sender, EventArgs eventargs);
    }
    public class Logging:ILogging, IDisposable
    {
        private const string OrangeColor = "#FFA500";
        private const string RedColor = "#FF0000";
        private static object _lockObject = new object();
        private readonly IIniSettings _iniSettings;
        private readonly IHSApplication _hs;
        private bool _disposed;
        private Logger _seriLogger = null;

        public Logging(IIniSettings iniSettings, IHSApplication hs)
        {
            _iniSettings = iniSettings;
            _hs = hs;

            if (_iniSettings.LogLevel == LogLevel.DebugToFile || _iniSettings.LogLevel == LogLevel.DebugToFileAndLog)
            {
                CreateLogFile();
            }
        }

        private void CreateLogFile()
        {
            if (_seriLogger != null) return;
            var logPath = Path.Combine(PluginConstants.ExePath, "Logs");
            if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);
            var logFile = Path.Combine(logPath, PluginConstants.PluginName + "Debug.log");
            lock (_lockObject)
            {
                _seriLogger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File(path: logFile, rollingInterval: RollingInterval.Day, shared: true)
                    .CreateLogger();
                var version = PluginConstants.PluginName + " version: " + Assembly.GetExecutingAssembly().GetName().Version;
                _seriLogger.Information($"{version}");
            }
        }

        private void CloseLogFile()
        {
            if (_seriLogger != null)
            {
                lock (_lockObject)
                {
                    _seriLogger.Dispose();
                    _seriLogger = null;
                }
            }
        }

        public void LogToHsNoMatterSettings(string logMessage)
        {
            DoLog(logMessage, LogLevel.IgnoreSettings);
        }

        public void LogError(string message)
        {
            //_hs.WriteLogEx(Utility.PluginName + "-Error", message, RedColor);
            DoLog(message, LogLevel.Error);
        }

        public void LogWarning(string message)
        {
            //_hs.WriteLogEx(Utility.PluginName + "-Warn", message, OrangeColor);
            DoLog(message, LogLevel.Warning);
        }

        public void LogInfo(string message)
        {
            DoLog(message, LogLevel.Info);
        }


        public void Log(string message)
        {
            DoLog(message, LogLevel.Normal);
        }

        public void Log(string message, LogLevel logLevel)
        {
            if (logLevel == _iniSettings.LogLevel)
                DoLog(message, logLevel);
        }

        public void LogDebug(string message)
        {
            DoLog(message, LogLevel.Debug);
        }


        public void LogIfLogToFileEnabled(string message, LogLevel msgLogLevel = LogLevel.Debug)
        {
            if (_seriLogger != null &&
                (_iniSettings.LogLevel == LogLevel.DebugToFile || _iniSettings.LogLevel == LogLevel.DebugToFileAndLog))
            {
                lock (_lockObject)
                {
                    switch (msgLogLevel)
                    {
                        case LogLevel.Warning:
                            _seriLogger.Warning($"{message}");
                            break;
                        case LogLevel.Normal:
                        case LogLevel.Info:
                            _seriLogger.Information($"{message}");
                            break;
                        case LogLevel.Error:
                            _seriLogger.Debug($"{message}");
                            break;
                        default:
                            _seriLogger.Debug($"{message}");
                            break;
                    }

                    Serilog.Log.CloseAndFlush();
                }
                //lock (_lockObject)
                //{
                //	_seriLogger.Debug($"{message}");
                //	Serilog.Log.CloseAndFlush();
                //}
            }
        }

        public void IniSettingHasChanged(object sender, EventArgs eventargs)
        {
            if (_iniSettings.LogLevel != LogLevel.DebugToFileAndLog && _iniSettings.LogLevel != LogLevel.DebugToFile)
            {
                CloseLogFile();
            }

            if (_iniSettings.LogLevel == LogLevel.DebugToFileAndLog || _iniSettings.LogLevel == LogLevel.DebugToFile)
            {
                CreateLogFile();
            }
        }

        public LogLevel LogLevel => _iniSettings.LogLevel;

        public bool IsLoggingToFile => (_iniSettings.LogLevel == LogLevel.DebugToFile ||
                                        _iniSettings.LogLevel == LogLevel.DebugToFileAndLog);

        public bool IsLoggingDebug => (IsLoggingToFile || _iniSettings.LogLevel == LogLevel.Debug);

        public void LogException(Exception ex)
        {
            if (ex != null)
            {
                _hs.WriteLog(PluginConstants.PluginName, "Error: " + ex.Message);
                _hs.WriteLog(PluginConstants.PluginName, ex.StackTrace);
            }
        }

        //private void DoLog(string message, LogLevel logLevel = LogLevel.Normal)
        //{
        //	if (_iniSettings.LogLevel == LogLevel.None && logLevel != LogLevel.IgnoreSettings) return; // Logging = off in config
        //																							   //Write to Homseseer log

        //	if ((logLevel == LogLevel.IgnoreSettings) || //Log that will be written no matter what ini setting
        //		(logLevel == LogLevel.Normal && _iniSettings.LogLevel == LogLevel.Normal) || //Log that will be written when ini setting is set to normal
        //		(logLevel == LogLevel.Debug && (_iniSettings.LogLevel == LogLevel.Debug ||
        //										_iniSettings.LogLevel == LogLevel.DebugToFileAndLog)))
        //	{
        //		_hs.WriteLog(Utility.PluginName, message);
        //	}

        //	//Write to logfile
        //	if (_iniSettings.LogLevel == LogLevel.DebugToFile || _iniSettings.LogLevel == LogLevel.DebugToFileAndLog)
        //	{
        //		LogIfLogToFileEnabled(message);
        //	}
        //}

        private void DoLog(string message, LogLevel msgLogLevel = LogLevel.Normal)
        {
            if (_iniSettings.LogLevel == LogLevel.None && msgLogLevel != LogLevel.IgnoreSettings) return; // Logging = off in config

            //Write to Homseseer log
            if (_iniSettings.LogLevel == LogLevel.Normal && msgLogLevel == LogLevel.Normal)
            {
                _hs.WriteLog(PluginConstants.PluginName, message);
            }

            if ((_iniSettings.LogLevel == LogLevel.Debug || _iniSettings.LogLevel == LogLevel.DebugToFileAndLog) &&
                (msgLogLevel == LogLevel.Normal || msgLogLevel == LogLevel.Debug))
            {
                _hs.WriteLog(PluginConstants.PluginName, message);
            }

            if (msgLogLevel == LogLevel.IgnoreSettings)  //Log that will be written no matter what ini setting))
            {
                _hs.WriteLog(PluginConstants.PluginName, message);
            }

            if (msgLogLevel == LogLevel.Warning)
            {
                _hs.WriteLogEx(PluginConstants.PluginName + "-Warn", message, OrangeColor);
            }

            if (msgLogLevel == LogLevel.Error)
            {
                _hs.WriteLogEx(PluginConstants.PluginName + "-Error", message, RedColor);
            }

            //Write to logfile
            if (_iniSettings.LogLevel == LogLevel.DebugToFile || _iniSettings.LogLevel == LogLevel.DebugToFileAndLog)
            {
                LogIfLogToFileEnabled(message, msgLogLevel);
            }
        }

        public void Dispose()
        {
            Dispose(true);

            // Use SupressFinalize in case a subclass 
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                // Indicate that the instance has been disposed.
                _disposed = true;
            }
        }
    }
    
}
