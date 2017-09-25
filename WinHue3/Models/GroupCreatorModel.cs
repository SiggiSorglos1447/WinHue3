using System.Collections.ObjectModel;
using WinHue3.Philips_Hue.HueObjects.LightObject;
using WinHue3.Validation;
using WinHue3.ViewModels;

namespace WinHue3.Models
{
    public class GroupCreatorModel : ValidatableBindableBase
    {

        
        private string _name;
        private ObservableCollection<dynamic> _listlights;
        private ObservableCollection<dynamic> _listAvailableLights;
        private string _type;
        private string _class;

        public GroupCreatorModel()
        {
            _listlights = new ObservableCollection<dynamic>();
            _listAvailableLights = new ObservableCollection<dynamic>();
            _type = "LightGroup";
            _class = "Other";
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        [MinimumCount(1, ErrorMessageResourceName = "Group_Select_One_Light", ErrorMessageResourceType = typeof(GlobalStrings))]
        public ObservableCollection<dynamic> Listlights
        {
            get => _listlights;
            set => SetProperty(ref _listlights,value);
        }

        public ObservableCollection<dynamic> ListAvailableLights
        {
            get => _listAvailableLights;
            set => SetProperty(ref _listAvailableLights,value);
        }

        public bool CanClass => Type == "Room";

        public string Type
        {
            get => _type;
            set
            {
                SetProperty(ref _type,value);
                if (value == "LightGroup")
                    Class = "Other";
                RaisePropertyChanged("CanClass");

            }
        }

        public string Class
        {
            get => _class;
            set => SetProperty(ref _class,value);
        }
    }
}
