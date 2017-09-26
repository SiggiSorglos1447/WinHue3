﻿using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Windows.Input;
using WinHue3.Models;
using WinHue3.Philips_Hue.HueObjects.GroupObject;
using WinHue3.Philips_Hue.HueObjects.LightObject;
using WinHue3.Utils;

namespace WinHue3.ViewModels
{
    public class GroupCreatorViewModel : ValidatableBindableBase
    {
        private GroupCreatorModel _groupCreator;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string _id;

        public GroupCreatorViewModel()
        {
            _groupCreator = new GroupCreatorModel();
            _id = string.Empty;
        }

        public dynamic Group
        {
            set
            {
                dynamic gr = value;
                GroupCreator.Name = gr.name;
                GroupCreator.Type = gr.type;
                _id = gr.Id;
                RaisePropertyChanged("CanChangeType");
                ObservableCollection<dynamic> list = new ObservableCollection<dynamic>();
                foreach (string s in gr.lights)
                {
                    if (GroupCreator.ListAvailableLights.Any(x => x.Id == s))
                        list.Add(GroupCreator.ListAvailableLights.Single(x => x.Id == s));
                    else
                    {
                        log.Error($"Light ID:{s} does not seems to exists anymore.");
                    }
                }
                GroupCreator.Listlights = list;
            }
            get
            {
                dynamic gr = new ExpandoObject();
                gr.name = GroupCreator.Name;
                gr.type = GroupCreator.Type;
                /*lights = GroupCreator.Listlights.Select(x => x.Id).ToList()*/
            
                if (_id != string.Empty)
                    gr.Id = _id;
                if (GroupCreator.Type == "Room")
                    gr.@class = GroupCreator.Class;
                return gr;
            }
        }

        public bool CanChangeType =>_id == string.Empty;
        
        public GroupCreatorModel GroupCreator
        {
            get => _groupCreator;
            set => SetProperty(ref _groupCreator, value);
        }

        private void ClearFields()
        {   
            GroupCreator.Listlights.Clear();
            GroupCreator.Name = string.Empty;
            GroupCreator.Type = "LightGroup";
            GroupCreator.Class = "Other";

        }

        public ICommand ClearFieldsCommand => new RelayCommand(param => ClearFields());


    }
}
