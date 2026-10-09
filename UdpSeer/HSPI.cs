using System.Collections.Generic;
using System.Collections.Specialized;
using HomeSeerAPI;
using Hspi;
using UdpSeer.IniSettings;
using UdpSeer.Logging;
using UdpSeer.Common;

namespace HSPI_UdpSeer
{
    public class HSPI : HspiBase
    {
        private IIniSettings _iniSettings;
        private ILogging _log;
        private IUdpListenerPlugin _plugin;

        public override string InitIO(string port)
        {
            // debug
            HS.WriteLog(PluginConstants.PluginName, "Entering InitIO");

            // initialise everything here, return a blank string only if successful, or an error message
            _iniSettings = new IniSettings(HS);
            _iniSettings.LoadSettingsFromIniFile();
            _log = new Logging(_iniSettings, HS);

            _ecowittPlugin = new EcowittPlugin(_log, HS, Callback, _iniSettings);//, new HsCollectionFactory());
            _ecowittPlugin.InitIO(port);

            // debug
            HS.WriteLog(PluginConstants.PluginName, "Completed InitIO");
            return "";
        }


        public override bool SupportsAddDevice()
        {
            throw new System.NotImplementedException();
        }

        public override IPlugInAPI.strInterfaceStatus InterfaceStatus()
        {
            throw new System.NotImplementedException();
        }

        public override void HSEvent(Enums.HSEvent eventType, object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public override string GenPage(string link)
        {
            throw new System.NotImplementedException();
        }

        public override string PagePut(string data)
        {
            throw new System.NotImplementedException();
        }

        public override void ShutdownIO()
        {
            throw new System.NotImplementedException();
        }

        public override bool RaisesGenericCallbacks()
        {
            throw new System.NotImplementedException();
        }

        public override void SetIOMulti(List<CAPI.CAPIControl> colSend)
        {
            throw new System.NotImplementedException();
        }



        public override IPlugInAPI.PollResultInfo PollDevice(int deviceId)
        {
            throw new System.NotImplementedException();
        }

        public override bool SupportsConfigDevice()
        {
            throw new System.NotImplementedException();
        }

        public override bool SupportsConfigDeviceAll()
        {
            throw new System.NotImplementedException();
        }

        public override Enums.ConfigDevicePostReturn ConfigDevicePost(int deviceId, string data, string user, int userRights)
        {
            throw new System.NotImplementedException();
        }

        public override string ConfigDevice(int deviceId, string user, int userRights, bool newDevice)
        {
            throw new System.NotImplementedException();
        }

        public override SearchReturn[] Search(string searchString, bool regEx)
        {
            throw new System.NotImplementedException();
        }

        public override object PluginFunction(string functionName, object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public override object PluginPropertyGet(string propertyName, object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public override void PluginPropertySet(string propertyName, object value)
        {
            throw new System.NotImplementedException();
        }

        public override void SpeakIn(int deviceId, string text, bool wait, string host)
        {
            throw new System.NotImplementedException();
        }

        public override int ActionCount()
        {
            throw new System.NotImplementedException();
        }

        public override bool ActionConfigured(IPlugInAPI.strTrigActInfo actionInfo)
        {
            throw new System.NotImplementedException();
        }

        public override string ActionBuildUI(string uniqueControlId, IPlugInAPI.strTrigActInfo actionInfo)
        {
            throw new System.NotImplementedException();
        }

        public override IPlugInAPI.strMultiReturn ActionProcessPostUI(NameValueCollection postData, IPlugInAPI.strTrigActInfo actionInfo)
        {
            throw new System.NotImplementedException();
        }

        public override string ActionFormatUI(IPlugInAPI.strTrigActInfo actionInfo)
        {
            throw new System.NotImplementedException();
        }

        public override bool ActionReferencesDevice(IPlugInAPI.strTrigActInfo actionInfo, int deviceId)
        {
            throw new System.NotImplementedException();
        }

        public override bool HandleAction(IPlugInAPI.strTrigActInfo actionInfo)
        {
            throw new System.NotImplementedException();
        }

        public override string TriggerBuildUI(string uniqueControlId, IPlugInAPI.strTrigActInfo triggerInfo)
        {
            throw new System.NotImplementedException();
        }

        public override IPlugInAPI.strMultiReturn TriggerProcessPostUI(NameValueCollection postData, IPlugInAPI.strTrigActInfo actionInfo)
        {
            throw new System.NotImplementedException();
        }

        public override string TriggerFormatUI(IPlugInAPI.strTrigActInfo actionInfo)
        {
            throw new System.NotImplementedException();
        }

        public override bool TriggerTrue(IPlugInAPI.strTrigActInfo actionInfo)
        {
            throw new System.NotImplementedException();
        }

        public override bool TriggerReferencesDevice(IPlugInAPI.strTrigActInfo actionInfo, int deviceId)
        {
            throw new System.NotImplementedException();
        }

        public override string GetPagePlugin(string page, string user, int userRights, string queryString)
        {
            throw new System.NotImplementedException();
        }

        public override string PostBackProc(string page, string data, string user, int userRights)
        {
            throw new System.NotImplementedException();
        }

        public override string get_ActionName(int actionNumber)
        {
            throw new System.NotImplementedException();
        }

        public override bool get_HasConditions(int triggerNumber)
        {
            throw new System.NotImplementedException();
        }

        public override string get_TriggerName(int triggerNumber)
        {
            throw new System.NotImplementedException();
        }

        public override int get_SubTriggerCount(int triggerNumber)
        {
            throw new System.NotImplementedException();
        }

        public override string get_SubTriggerName(int triggerNumber, int subTriggerNumber)
        {
            throw new System.NotImplementedException();
        }

        public override bool get_TriggerConfigured(IPlugInAPI.strTrigActInfo actionInfo)
        {
            throw new System.NotImplementedException();
        }

        public override bool get_Condition(IPlugInAPI.strTrigActInfo actionInfo)
        {
            throw new System.NotImplementedException();
        }

        public override void set_Condition(IPlugInAPI.strTrigActInfo actionInfo, bool value)
        {
            throw new System.NotImplementedException();
        }

        protected override bool GetHasTriggers()
        {
            throw new System.NotImplementedException();
        }

        protected override int GetTriggerCount()
        {
            throw new System.NotImplementedException();
        }

        protected override string GetName()
        {
            throw new System.NotImplementedException();
        }

        protected override bool GetHscomPort()
        {
            throw new System.NotImplementedException();
        }

        public override void SetDeviceValue(int deviceId, double value, bool trigger = true)
        {
            throw new System.NotImplementedException();
        }

        public override string InstanceFriendlyName()
        {
            throw new System.NotImplementedException();
        }

        public override int Capabilities()
        {
            throw new System.NotImplementedException();
        }

        public override int AccessLevel()
        {
            throw new System.NotImplementedException();
        }

        public override bool SupportsMultipleInstances()
        {
            throw new System.NotImplementedException();
        }

        public override bool SupportsMultipleInstancesSingleEXE()
        {
            throw new System.NotImplementedException();
        }
    }
}