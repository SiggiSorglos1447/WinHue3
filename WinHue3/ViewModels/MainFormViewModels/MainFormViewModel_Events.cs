﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WinHue3.ExtensionMethods;
using WinHue3.Interface;
using WinHue3.Philips_Hue.BridgeObject;
using WinHue3.Philips_Hue.Communication;
using WinHue3.Philips_Hue.HueObjects.Common;
using WinHue3.Utils;


namespace WinHue3.ViewModels.MainFormViewModels
{
    public partial class MainFormViewModel
    {
        private void Bridge_OnMessageAdded(object sender, EventArgs e)
        {
            if (SelectedBridge.LastCommandMessages?.ListMessages.Count > 0)
            {
                Lastmessage = SelectedBridge.LastCommandMessages.LastError?.ToString() ?? SelectedBridge.LastCommandMessages.LastSuccess.ToString();
            }
               
        }

        private void Bridge_BridgeNotResponding(object sender, EventArgs e)
        {

            MessageBox.Show(GlobalStrings.Error_Bridge_Not_Responding, GlobalStrings.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            if (e is BridgeNotRespondingEventArgs)
            {
                log.Error($"{sender}");
                log.Error(Serializer.SerializeToJson(((BridgeNotRespondingEventArgs)e).ex?.ToString()));
            }
            log.Error($"{sender} : {e}");
            Cursor_Tools.ShowNormalCursor();

        }

        private async void _findlighttimer_Tick(object sender, EventArgs e)
        {
            _findlighttimer.Stop();
            log.Info("Done searching for new lights.");
            List<dynamic> hr = await HueObjectHelper.GetBridgeNewLightsAsyncTask(SelectedBridge);
            if (hr == null) return;
            log.Info($"Found {hr.Count} new lights.");
            ListBridgeObjects.AddRange(hr);
            CommandManager.InvalidateRequerySuggested();
            RaisePropertyChanged("SearchingLights");
        }

        private async void _findsensortimer_Tick(object sender, EventArgs e)
        {
            _findsensortimer.Stop();
            log.Info("Done searching for new sensors.");
            List<dynamic> hr = await HueObjectHelper.GetBridgeNewSensorsAsyncTask(SelectedBridge);
            if (hr == null) return;
            log.Info($"Found {hr.Count} new sensors.");
            ListBridgeObjects.AddRange(hr);
            CommandManager.InvalidateRequerySuggested();
            RaisePropertyChanged("SearchingLights");
        }

    }
}
