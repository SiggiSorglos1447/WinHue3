﻿using System;
using System.Net;
using WinHue3.Philips_Hue.BridgeObject.BridgeMessages;
using WinHue3.Philips_Hue.Communication;
using WinHue3.ViewModels;

namespace WinHue3.Philips_Hue.BridgeObject
{

    /// <summary>
    /// Bridge Class.
    /// </summary>
    public partial class Bridge : ValidatableBindableBase
    {
        private Messages _lastCommandMessages;
        private string _apiKey = String.Empty;
        readonly EventArgs _e = null;
        private string _apiversion = String.Empty;
        private string _mac = String.Empty;
        private string _swversion;
        private bool _isdefault;
        private IPAddress _ipAddress;
        private string _name;

        /// <summary>
        /// Api Key to access the bridge. If the application is not autorized the api key will not be set.
        /// </summary>
        public string ApiKey
        {
            get => _apiKey;
            set => SetProperty(ref _apiKey, value);
        }

        /// <summary>
        /// Is the default bridge.
        /// </summary>        
        public bool IsDefault
        {
            get => _isdefault;
            set => SetProperty(ref _isdefault,value);
        }

        public string name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// Software version of the bridge.
        /// </summary>
        public string SwVersion
        {
            get => _swversion;
            set => SetProperty(ref _swversion,value);
        }

        /// <summary>
        /// Mac address of the bridge.
        /// </summary>
        public string Mac
        {
            get => _mac;
            set => SetProperty(ref _mac,value);
        }

        /// <summary>
        /// Version of the bridge api
        /// </summary>
        public string ApiVersion
        {
            get => _apiversion;
            set => SetProperty(ref _apiversion,value);
        }

        /// <summary>
        /// IP Address of the bridge.
        /// </summary>
        public IPAddress IpAddress
        {
            get => _ipAddress;
            set => SetProperty(ref _ipAddress,value);
        }

        public string LongName => $"{name} ({IpAddress})";

        /// <summary>
        /// Return the full url with IP Address and Api key to the bridge.
        /// </summary>
        public string BridgeUrl => _apiKey != String.Empty ? $"http://{_ipAddress }/api/{_apiKey}" : $"http://{_ipAddress}/api";

        public Messages LastCommandMessages
        {
            get => _lastCommandMessages;
            set => SetProperty(ref _lastCommandMessages,value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Bridge()
        {
            IpAddress = IPAddress.None;
            ApiKey = String.Empty;
            LastCommandMessages = new Messages();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ip">IP Address of the bridge</param>
        /// <param name="apiKey">[Optional] The Api to access the bridge.</param>
        public Bridge(IPAddress ip, string mac, string apiversion, string swversion,string newname, string apiKey = null)
        {
            IpAddress = ip;
            if (apiKey != null || apiKey != string.Empty)
            {
                ApiKey = apiKey;
            }
            Mac = mac;
            ApiVersion = apiversion;
            SwVersion = swversion;
            name = newname;
            LastCommandMessages = new Messages();
        }

    }

    public class BridgeNotRespondingEventArgs : EventArgs
    {
        public CommResult ex;
    }
}
