﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WinHue3.Philips_Hue.BridgeObject;
using WinHue3.Philips_Hue.HueObjects.SceneObject;
using WinHue3.Settings;
using WinHue3.Utils;
using WinHue3.ViewModels;


namespace WinHue3.Views
{
    /// <summary>
    /// Interaction logic for Form_HueTapConfig.xaml
    /// </summary>
    public partial class Form_HueTapConfig : Window
    {
        private Bridge _bridge;
        HueTapConfigViewModel tcvm;
        public Form_HueTapConfig()
        {

            InitializeComponent();
            tcvm = DataContext as HueTapConfigViewModel;  
        }

        public async Task Initialize(string sensorid, Bridge bridge)
        {
            _bridge = bridge;
            tcvm.Bridge = bridge;
            tcvm.HueTapModel.Id = sensorid;

            List<dynamic> hr = await HueObjectHelper.GetBridgeScenesAsyncTask(_bridge);

            if (hr != null)
            {

                if (WinHueSettings.settings.ShowHiddenScenes)
                {
                    tcvm.HueTapModel.ListScenes = hr;
                }
                else
                {
                    List<dynamic> temp = hr;
                    temp = temp.Where(x => !x.name.StartsWith("HIDDEN")).ToList();
                    tcvm.HueTapModel.ListScenes = temp;
                }

            }
            else
            {
                _bridge.ShowErrorMessages();
            }
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

    }
}
