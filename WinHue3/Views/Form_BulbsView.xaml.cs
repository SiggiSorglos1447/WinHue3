﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using WinHue3.Philips_Hue.BridgeObject;
using WinHue3.Philips_Hue.HueObjects.LightObject;
using WinHue3.Utils;
using WinHue3.ViewModels;


namespace WinHue3.Views
{
    /// <summary>
    /// Interaction logic for Form_BulbsView.xaml
    /// </summary>
    public partial class Form_BulbsView : Window
    {
        private BulbsViewViewModel _bvv;
        private Bridge _bridge;

        public Form_BulbsView()
        {
            InitializeComponent();
            _bvv = DataContext as BulbsViewViewModel;
        }

        public async Task Initialize(Bridge bridge)
        {
            _bridge = bridge;
            List<dynamic> lresult = await HueObjectHelper.GetBridgeLightsAsyncTask(_bridge);
            if (lresult == null) return;
            _bvv.Initialize(lresult);
        }

    }
}
