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
using UventoXF.Models;
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
            EventTypes = new ObservableCollection<EventType>();
            EventItems = new ObservableCollection<EventItem>();
            //Dates = new BindingList<DateItem>();
            Date = new BindingList<DateItem>();
            Date.ListChanged += OnBindingListChanged;
            loadEventItems();
            NavigateToTasbeehPageCommand = new Command(async () => await ExecuteNavigateToTasbeehPageCommand());
            NavigateToQiblaPageCommand = new Command(async () => await ExecuteNavigateToQiblaPageCommand());
            NavigateTo99NamesPageCommand = new Command(async () => await ExecuteNavigateTo99NamesPageCommand());
        }
        public double current_latitude { get; set; }
        public double current_longitude { get; set; }


        public Command getLocationData { get; }
        public Location location;
        public Command NavigateToTasbeehPageCommand { get; }
        public Command NavigateToQiblaPageCommand { get; }
        public Command NavigateTo99NamesPageCommand { get; }
        public Command SelectDateCommand { get; }
        public Command SelectEventTypeCommand { get; }
        public ObservableCollection<EventType> EventTypes { get; }
        public ObservableCollection<EventItem> EventItems { get; }
        public BindingList<DateItem> Date;
        public BindingList<DateItem> Dates { get { return Date; } }
        CancellationTokenSource cts;
        private DateItem _selectedDate;

        private static void OnBindingListChanged(object sender, ListChangedEventArgs e)
        {
            // Event raised.  
            // Now do your stuff...
            Console.WriteLine("Item in the collection has been changed, handling this event.");
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

        public DateItem SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        private EventType _selectedEventType;
        public EventType SelectedEventType
        {
            get => _selectedEventType;
            set => SetProperty(ref _selectedEventType, value);
        }

        private void loadEventItems()
        {
            EventItems.Add(new EventItem()
            {
                title = "13 Line Quraan",
                //date = "Jan 12, 2021",
                //location = "Greenfields, Sector 42, Faridabad",
                image = "event1.png"
            });

            EventItems.Add(new EventItem()
            {
                title = "Duaa Kitaab - The Weapon of a Believer",
                //date = "Jan 12, 2021",
                //location = "Galaxyfields, Sector 22, Faridabad",
                image = "event2.png"
            });

            EventItems.Add(new EventItem()
            {
                title = "Duaas for the Contentment of the Heart",
                //date = "Jan 12, 2021",
                //location = "Greenfields, Sector 42, Faridabad",
                image = "event3.png"
            });
        }
    }
}
