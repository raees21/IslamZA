using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using UventoXF.Helpers;
using UventoXF.ViewModel;
using UventoXF.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UventoXF.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {

        public MainPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            NavigateToTasbeehPageCommand = new Command(async () => await ExecuteNavigateToTasbeehPageCommand());
            NavigateToQiblaPageCommand = new Command(async () => await ExecuteNavigateToQiblaPageCommand());
            NavigateTo99NamesPageCommand = new Command(async () => await ExecuteNavigateTo99NamesPageCommand());
            NavigateToMufiAKDuas = new Command(async () => await ExecuteNavigateMuftiAKDuas());
        }
        public double current_latitude { get; set; }
        public double current_longitude { get; set; }


        public Command getLocationData { get; }
        public Location location;
        public Command NavigateToTasbeehPageCommand { get; }
        public Command NavigateToQiblaPageCommand { get; }
        public Command NavigateTo99NamesPageCommand { get; }
        public Command NavigateToMufiAKDuas { get; }

        private async Task ExecuteNavigateMuftiAKDuas()
        {
            await Navigation.PushAsync(new MufiAkDuas());
        }

        private async Task ExecuteNavigateToQiblaPageCommand()
        {
            await Navigation.PushAsync(new Page2());
        }

        private async Task ExecuteNavigateToTasbeehPageCommand()
        {
            await Navigation.PushAsync(new Page1());
        }
        private async Task ExecuteNavigateTo99NamesPageCommand()
        {
            await Navigation.PushAsync(new Page3());
        }
    }
}
