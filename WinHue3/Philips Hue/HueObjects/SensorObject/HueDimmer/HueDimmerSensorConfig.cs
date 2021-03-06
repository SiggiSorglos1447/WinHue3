﻿using System.ComponentModel;
using System.Runtime.Serialization;
using WinHue3.Philips_Hue.Communication;
using WinHue3.Philips_Hue.HueObjects.Common;
using WinHue3.ViewModels;

namespace WinHue3.Philips_Hue.HueObjects.SensorObject.HueDimmer
{
    [DataContract]
    public class HueDimmerSensorConfig : ValidatableBindableBase, ISensorConfig
    {
        private string _alert;
        private bool? _on;
        private bool? _reachable;
        private byte? _battery;

        /// <summary>
        /// Alert.
        /// </summary>
        [HueProperty, DataMember]
        public string alert
        {
            get => _alert;
            set => SetProperty(ref _alert,value);
        }

        /// <summary>
        /// On off state.
        /// </summary>
        [HueProperty, DataMember]
        public bool? on
        {
            get => _on;
            set => SetProperty(ref _on,value);
        }

        /// <summary>
        /// Sensor reachability.
        /// </summary>
        [HueProperty, DataMember, ReadOnly(true)]
        public bool? reachable
        {
            get => _reachable;
            set => SetProperty(ref _reachable,value);
        }

        /// <summary>
        /// Battery state.
        /// </summary>
        [HueProperty, DataMember,ReadOnly(true)]
        public byte? battery
        {
            get => _battery;
            set => SetProperty(ref _battery,value);
        }

        public override string ToString()
        {
            return Serializer.SerializeToJson(this);
        }
    }
}
