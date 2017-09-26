﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using System.Windows;
using WinHue3.Philips_Hue.BridgeObject;
using WinHue3.Philips_Hue.BridgeObject.BridgeObjects;
using WinHue3.Philips_Hue.HueObjects.LightObject;
using WinHue3.Philips_Hue.HueObjects.SceneObject;
using WinHue3.Utils;
using WinHue3.ViewModels;
using WinHue3.Philips_Hue;
namespace WinHue3.Views
{   

    /// <summary>
    /// Interaction logic for Form_SceneMapping.xaml
    /// </summary>
    public partial class Form_SceneMapping : Window
    {

        private readonly SceneMappingViewModel _smv;
        private Bridge _bridge;
        public Form_SceneMapping()
        {
            InitializeComponent();
            _smv = DataContext as SceneMappingViewModel;
        }

        public async Task Initialize(Bridge bridge)
        {
            _bridge = bridge;
            Dictionary<string, ExpandoObject> lresult = await _bridge.GetListObjectsAsyncTask(HueObjectType.lights);
            if (lresult != null)
            {
                Dictionary<string, ExpandoObject> sresult = await _bridge.GetListObjectsAsyncTask(HueObjectType.scenes);
                if (sresult != null)
                {
                    
                    _smv.Initialize(sresult, lresult, _bridge);
                }
                else
                {
                    MessageBoxError.ShowLastErrorMessages(bridge);
                }
            }
            else
            {
                MessageBoxError.ShowLastErrorMessages(bridge);
            }
        }

        private void dgListScenes_ItemsSourceChangeCompleted(object sender, EventArgs e)
        {
            if (dgListScenes.Columns.Count < 1) return;
            dgListScenes.Columns[0].Visible = false;
        }

    }
}
