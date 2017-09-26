﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using OpenHardwareMonitor.Hardware;
using WinHue3.Philips_Hue.BridgeObject;
using WinHue3.Philips_Hue.HueObjects.Common;
using WinHue3.Philips_Hue.HueObjects.GroupObject;
using WinHue3.Philips_Hue.HueObjects.LightObject;
using WinHue3.Utils;
using WinHue3.ViewModels;
using WinHue3.Philips_Hue;

namespace WinHue3.Addons.CpuTempMon
{
    public class CpuTempViewModel : ValidatableBindableBase
    {
        private int _lowerTemp;
        private int _UpperTemp;
        private int _lowerGradientColor;
        private int _upperGradientColor;
        private Bridge _bridge;
        private CpuTemp _temp;
        public bool _canTest;
        public string _cpuTemp;
        public ISensor _selectedSensor;
        public dynamic _selectedObject;
        public byte _bri;
        public byte _sat;
        public List<dynamic> _listLightGroups;
        public ObservableCollection<ISensor> _listCpuSensors;

        public CpuTempViewModel()
        {
            LowerTemp = 30;
            UpperTemp = 75;
            CanTest = true;
            LowerGradientColor = 25000;
            UpperGradientColor = 0;
            CpuTemp = "0.00ºC";
            Bri = 254;
            Sat = 254;
            ListLightGroups = new List<dynamic>();
            ListCpuSensors = new ObservableCollection<ISensor>();
        }

        public void Initialize(Bridge bridge, CpuTemp temp)
        {
            _bridge = bridge;
            _temp = temp;
            _temp.OnTempUpdated += _temp_OnTempUpdated;
            _temp.OnSensorUpdated += _temp_OnSensorUpdated;
            _temp.Start();

            List<dynamic> hr = HueObjectHelper.GetBridgeDataStore(_bridge);

            if (hr == null) return;
            ListLightGroups.AddRange(hr.Where(x => x.huetype == HueObjectType.lights));
            ListLightGroups.AddRange(hr.Where(x => x is Group));

            SelectedObject = Properties.Settings.Default.CPUTemp_ObjectID != "" ? ListLightGroups.Find(x => x.Id == Properties.Settings.Default.CPUTemp_ObjectID) : ListLightGroups[0];
        }

        private void _temp_OnSensorUpdated(object sender, EventArgs e)
        {
            ListCpuSensors = Temp.CpuSensors;
            RaisePropertyChanged("CanSelectSensor");
            SelectedSensor = Temp.CpuSensors.FirstOrDefault(x => x.Name.Contains("Package"));
            _temp.OnSensorUpdated -= _temp_OnSensorUpdated;
        }

        private void _temp_OnTempUpdated(object sender, CpuTempEventArgs e)
        {
        
            float? actualtemp = e.currentTemp;
            ushort hueTemp = 0;

            CpuTemp = $"{Temp.Temperature:0.0}ºC";
            if (CanTest) return;
         

            if (actualtemp == null)
            {
                hueTemp = (ushort)_lowerGradientColor;
            }
            else
            {

                double gradientRange = _lowerGradientColor - _upperGradientColor;
                double tempRange = _UpperTemp - _lowerTemp;


                if (gradientRange > 0)
                {
                    double multiplier = _lowerGradientColor / tempRange;
                    hueTemp = (ushort)(_lowerGradientColor - (multiplier * (actualtemp - _lowerTemp)));
                }
                else
                {
                    hueTemp = (ushort)((_upperGradientColor * actualtemp) / _UpperTemp);
                }

            }



            if (SelectedObject.huetype == HueObjectType.lights)
            {
                _bridge.SetState(new State() { hue = hueTemp, bri = Bri, sat = Sat, @on = true, transitiontime = 9 }, _selectedObject.Id);
            }
            else
            {
                _bridge.SetState(new Philips_Hue.HueObjects.GroupObject.Action() { hue = hueTemp, bri = Bri, sat = Sat, @on = true, transitiontime = 9 }, _selectedObject.Id);
            }

            
        }

        public bool CanSelectSensor => ListCpuSensors.Count > 0;

        public int LowerTemp
        {
            get => _lowerTemp;
            set => SetProperty(ref _lowerTemp,value);
        }

        public int UpperTemp
        {
            get => _UpperTemp;
            set => SetProperty(ref _UpperTemp,value);
        }

        public int LowerGradientColor
        {
            get => _lowerGradientColor;
            set => SetProperty(ref _lowerGradientColor,value);
        }

        public int UpperGradientColor
        {
            get => _upperGradientColor;
            set => SetProperty(ref _upperGradientColor,value);
        }

        public CpuTemp Temp
        {
            get => _temp;
            set => SetProperty(ref _temp,value);
        }

        public bool CanTest
        {
            get => _canTest;
            set => SetProperty(ref _canTest, value);
        }

        public string CpuTemp
        {
            get => _cpuTemp;
            set => SetProperty(ref _cpuTemp,value);
        }

        public ISensor SelectedSensor
        {
            get => _selectedSensor;
            set => SetProperty(ref _selectedSensor, value);
        }

        public dynamic SelectedObject
        {
            get => _selectedObject;
            set => SetProperty(ref _selectedObject, value);
        }

        public byte Bri
        {
            get => _bri;
            set => SetProperty(ref _bri,value);
        }

        public byte Sat
        {
            get => _sat;
            set => SetProperty(ref _sat,value);
        }

        public List<dynamic> ListLightGroups
        {
            get => _listLightGroups;
            set => SetProperty(ref _listLightGroups, value);
        }

        public ICommand TestCpuTempCommand => new RelayCommand(param => TestCpuTemp());

        public ObservableCollection<ISensor> ListCpuSensors
        {
            get => _listCpuSensors;
            set => SetProperty(ref _listCpuSensors,value);
        }

        private void TestCpuTemp()
        {
            CanTest = !CanTest;
        }

    }
}
