using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpSeer.Common
{
    public class PluginConstants
    {
        public const string Inifile = "UdpSeer.ini";
        public const string IsoDateTimeFormatting = "yyyy-MM-dd HH:mm:ss";
        public static string ExePath = Directory.GetCurrentDirectory();
        public static string InstanceFriendlyName => "";
        public static string PluginName = "UdpSeer";
        public static string UdpSeerIdPedPassKey => "UdpSeerIdPassKey";
        public static string PedIsUdpSeerDataKey => "IsUdpSeerData";
        public static string HsNameSeparator = ";";
        public static string HsRootMarker => "root";
        public static string PluginInstanceName => "";
        public static bool IsRunningOnMono => (Type.GetType("Mono.Runtime") != null);
        public static string ConnectedString => "Connected";
        public static string PedDeviceTypeKey => "DeviceTypeKey";



        public static string PedVersionKey = "UdpSeerVersion";

        //Upgrade versions
        public static string AddPasskeyToAddressVersionAsString = "0.0.2.0";
        public static int AddPasskeyToAddressVersionAsInt = 200;

        public static int MaxUpdateVersionAsInt = 200;


        //Unit types
        public static string PasskeyUnitString = "PASSKEY";
        public static string StationTypeUnitString = "stationtype";
        public static string DateUtcUnitString = "dateutc";
        public static string SoilMoistureUnitString = "soilmoisture";
        public static string HumidityUnitString = "humidity";
        public static string TemperatureUnitString = "temp";
        public static string TemperatureInFahrenheitUnitString = "temp";
        public static string WindchillFahrenheitUnitString = "windchillf";
        public static string DewpointfInFahrenheitUnitString = "dewptf";
        public static string BatteryUnitString = "batt";
        public static string BatteryWh65UnitString = "wh65batt";
        public static string BatteryWh25UnitString = "wh25batt";
        public static string BarometerAbsoluttInchUnitString = "baromabsin";
        public static string BarometerRelativeInchUnitString = "baromrelin";
        public static string FrequencyUnitString = "freq";
        public static string ModelUnitString = "model";
        public static string WindDirectionUnitString = "winddir";
        public static string SoilmoistureBatteryUnitString = "soilbatt";
        public static string WindDirectionAverage10MinutesUnitString = "winddir_avg10m";
        public static string WindgustMphUnitString = "windgustmph";
        public static string WindSpeedMphAvg10MinutesUnitString = "windspdmph_avg10m";
        public static string WindSpeedMphUnitString = "windspeedmph";
        public static string MaxDailyGustUnitString = "maxdailygust";
        public static string YearlyRainInchUnitString = "yearlyrainin";
        public static string WeeklyRainInchUnitString = "weeklyrainin";
        public static string DailyRainInchUnitString = "dailyrainin";
        public static string EventRainInchUnitString = "eventrainin";
        public static string HourlyRainInchUnitString = "hourlyrainin";
        public static string MonthlyRainInchUnitString = "monthlyrainin";
        public static string RainRateInchUnitString = "rainratein";
        public static string TotalRainUnitString = "totalrainin";
        public static string SolarRadiationUnitString = "solarradiation";
        public static string UltraVioletUnitString = "uv";
        public static string ConnectionDeviceString = "ConnectionDevice";

    }
}
