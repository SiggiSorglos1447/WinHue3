﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WinHue3.ExtensionMethods;
using WinHue3.Interface;
using WinHue3.Models;
using WinHue3.Philips_Hue.BridgeObject;
using WinHue3.Philips_Hue.HueObjects.Common;
using WinHue3.Philips_Hue.HueObjects.GroupObject;
using WinHue3.Philips_Hue.HueObjects.LightObject;
using WinHue3.Philips_Hue.HueObjects.NewSensorsObject;
using WinHue3.Philips_Hue.HueObjects.ResourceLinkObject;
using WinHue3.Philips_Hue.HueObjects.RuleObject;
using WinHue3.Philips_Hue.HueObjects.SceneObject;
using WinHue3.Philips_Hue.HueObjects.ScheduleObject;
using WinHue3.Settings;
using WinHue3.Utils;
using WinHue3.ViewModels;


namespace WinHue3.Views
{
    /// <summary>
    /// Interaction logic for Form_ResourceLinksCreator.xaml
    /// </summary>
    public partial class Form_ResourceLinksCreator : Window
    {
        private ResourceLinkCreatorViewModel rlcvm;
        private Bridge _bridge;
        private string id;

        public Form_ResourceLinksCreator()
        {        
            InitializeComponent();
            rlcvm = this.DataContext as ResourceLinkCreatorViewModel;
        }

        public async Task Initialize(Bridge bridge, Resourcelink rl = null)
        {
            _bridge = bridge;
           
            rlcvm.LinkCreatorModel.ShowID = WinHueSettings.settings.ShowID;
            rlcvm.LinkCreatorModel.Wrap = WinHueSettings.settings.WrapText;

            List<dynamic> hr = await HueObjectHelper.GetBridgeDataStoreAsyncTask(_bridge);
            if (hr == null) return;
            ObservableCollection<dynamic> listbrobj = new ObservableCollection<dynamic>();
            List<dynamic> listobj = hr;

            switch (WinHueSettings.settings.Sort)
            {
                case WinHueSortOrder.Default:
                    listbrobj = new ObservableCollection<dynamic>(hr);
                    break;
                case WinHueSortOrder.Ascending:
                    listbrobj.AddRange(from item in listobj where item is Light orderby item.name ascending select item);
                    listbrobj.AddRange(from item in listobj where item is Group orderby item.name ascending select item);
                    listbrobj.AddRange(from item in listobj where item is Schedule orderby item.name ascending select item);
                    listbrobj.AddRange(from item in listobj where item is Scene orderby item.name ascending select item);
                    listbrobj.AddRange(from item in listobj where item is Sensor orderby item.name ascending select item);
                    listbrobj.AddRange(from item in listobj where item is Rule orderby item.name ascending select item);
                    listbrobj.AddRange(from item in listobj where item is Resourcelink orderby item.name ascending select item);
                    break;
                case WinHueSortOrder.Descending:
                    listbrobj.AddRange(from item in listobj where item is Light orderby item.name descending select item);
                    listbrobj.AddRange(from item in listobj where item is Group orderby item.name descending select item);
                    listbrobj.AddRange(from item in listobj where item is Schedule orderby item.name descending select item);
                    listbrobj.AddRange(from item in listobj where item is Scene orderby item.name descending select item);
                    listbrobj.AddRange(from item in listobj where item is Sensor orderby item.name descending select item);
                    listbrobj.AddRange(from item in listobj where item is Rule orderby item.name descending select item);
                    listbrobj.AddRange(from item in listobj where item is Resourcelink orderby item.name descending select item);
                    break;
                default:
                    goto case WinHueSortOrder.Default;
            }
            rlcvm.ListHueObjects = listbrobj;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(rlcvm.ListHueObjects);
            view.GroupDescriptions?.Clear();
            PropertyGroupDescription groupDesc = new TypeGroupDescription();
            view.GroupDescriptions?.Add(groupDesc);

            CollectionView view2 = (CollectionView)CollectionViewSource.GetDefaultView(rlcvm.LinkCreatorModel.ListlinkObject);
            view2.GroupDescriptions?.Clear();
            PropertyGroupDescription groupDesc2 = new TypeGroupDescription();
            view2.GroupDescriptions?.Add(groupDesc2);
            if (rl != null)
                rlcvm.Resourcelink = rl;
        }

        private void btnCreateResourceLink_Click(object sender, RoutedEventArgs e)
        {
            Resourcelink rl = rlcvm.Resourcelink;
            bool result = rlcvm.IsEditing ? _bridge.ModifyObject(rl) : _bridge.CreateObject(rl);
            
            
            if (result)
            {
                id = rlcvm.IsEditing ? rl.Id : _bridge.LastCommandMessages.LastSuccess.value;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBoxError.ShowLastErrorMessages(_bridge);
            }

        }

        public string GetCreatedModifiedId()
        {
            return id;
        }
        

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
