﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using WinHue3.Philips_Hue.BridgeObject;
using WinHue3.Philips_Hue.HueObjects.GroupObject;
using WinHue3.Philips_Hue.HueObjects.LightObject;
using WinHue3.Utils;
using WinHue3.ViewModels;

namespace WinHue3.Views
{
    /// <summary>
    /// Interaction logic for Form_BulbsView.xaml
    /// </summary>
    public partial class Form_GroupView : Window
    {
        private GroupViewViewModel _gvv;
        private Bridge _bridge;
        public Form_GroupView()
        {
            InitializeComponent();
        }

        public async Task Initialize(Bridge bridge)
        {
            _bridge = bridge;
            List<dynamic> comlgt = await HueObjectHelper.GetBridgeLightsAsyncTask(_bridge);
            _gvv = DataContext as GroupViewViewModel;

            if (comlgt != null)
            {
                List<Group> comgrp = await HueObjectHelper.GetBridgeGroupsAsyncTask(_bridge);
                if (comgrp != null)
                {

                    _gvv.Initialize(comgrp, comlgt);
                }
                else
                {
                    MessageBoxError.ShowLastErrorMessages(bridge);
                }

                DataContext = _gvv;

            }
            else
            {
                MessageBoxError.ShowLastErrorMessages(bridge);
            }
        }

    }
}
