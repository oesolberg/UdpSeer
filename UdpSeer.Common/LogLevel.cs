namespace UdpSeer.Common
{
    public enum LogLevel
    {
        None = 0,
        Normal = 1,
        Debug = 2,
        DebugToFile = 3,
        DebugToFileAndLog = 4,
        IgnoreSettings,
        Error,
        Warning,
        Info
    }
}