using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            GetLocation();
            EventTypes = new ObservableCollection<EventType>();
            EventItems = new ObservableCollection<EventItem>();
            Dates = new ObservableCollection<DateItem>();
            SelectDateCommand = new Command<DateItem>((model) => ExecuteSelectDateCommand(model));
            SelectEventTypeCommand = new Command<EventType>((model) => ExecuteSelectEventTypeCommand(model));
            loadEventTypes();
            loadEventItems();
            loadDates();
            NavigateToTasbeehPageCommand = new Command(async () => await ExecuteNavigateToTasbeehPageCommand());
            NavigateToQiblaPageCommand = new Command(async () => await ExecuteNavigateToQiblaPageCommand());
            NavigateTo99NamesPageCommand = new Command(async () => await ExecuteNavigateTo99NamesPageCommand());
        }
        double current_latitude;
        double current_longitude;
        public Command NavigateToTasbeehPageCommand { get; }
        public Command NavigateToQiblaPageCommand { get; }
        public Command NavigateTo99NamesPageCommand { get; }
        public Command SelectDateCommand { get; }
        public Command SelectEventTypeCommand { get; }
        public ObservableCollection<EventType> EventTypes { get; }
        public ObservableCollection<EventItem> EventItems { get; }
        public ObservableCollection<DateItem> Dates { get; }
        CancellationTokenSource cts;


        private DateItem _selectedDate;

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

        private void loadEventTypes()
        {
            EventTypes.Add(new EventType()
            {
                name = "Tasbeeh",
                //image = "mic.png",
                selected = true,
                backgroundColor = "#FCCD00",
                textColor = "#000000"

            });

            EventTypes.Add(new EventType()
            {
                name = "99 Names",
                //image = "ping_pong.png",
                backgroundColor = "#29404E",
                textColor = "#FFFFFF"
            });

            EventTypes.Add(new EventType()
            {
                name = "Qiblah",
                //image = "graduation.png",
                backgroundColor = "#29404E",
                textColor = "#FFFFFF"
            });
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

        private static T _download_serialized_json_data<T>(string url) where T : new()
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }

        public async Task GetLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);
            current_latitude = location.Latitude;
            current_longitude = location.Longitude;
        }

        private void loadDates()
        {

            //int indexVal = 0;
            //var url = "https://api.aladhan.com/v1/calendar?latitude="+current_latitude+"&longitude="+current_longitude+"&method=1&month=7&year=2021";
            ////var url = "https://api.aladhan.com/v1/calendar?latitude=-28.225749969482422&longitude=28.30585479736328&method=1&month=7&year=2021";
            //var timings = _download_serialized_json_data<Rootobject>(url);
            //var xml = XDocument.Load("http://api.timezonedb.com/v2.1/get-time-zone?key=HITW6BYBQTCK&format=xml&by=position&lat=" + current_latitude + "&lng="+current_longitude);

            //string currentDate = xml.Root.LastNode.ToString().Substring(11, 10);

            //currentDate = currentDate.Substring(8, 2) + "-" + currentDate.Substring(5, 2) + "-" + currentDate.Substring(0, 4);
            //currentDate = currentDate.Trim();

            //for (int i = 0; i < timings.data.Length; i++)
            //{
            //    if (currentDate.Equals(timings.data[i].date.gregorian.date))
            //    {
            //        indexVal = i;
            //    }
            //}


            var namaazList = new List<string>();

            namaazList.Add("Fajr");
            namaazList.Add("Zohr");
            namaazList.Add("Asr");
            namaazList.Add("Maghrib");
            namaazList.Add("Esha");

            //var namaazTime = new List<string>();

            //namaazTime.Add(timings.data[indexVal].timings.Fajr.Substring(0, 5));
            //namaazTime.Add(timings.data[indexVal].timings.Dhuhr.Substring(0, 5));
            //namaazTime.Add(timings.data[indexVal].timings.Asr.Substring(0, 5));
            //namaazTime.Add(timings.data[indexVal].timings.Maghrib.Substring(0, 5));
            //namaazTime.Add(timings.data[indexVal].timings.Isha.Substring(0, 5));

            for (int i = 1; i <= 5; i++)
            {
                Dates.Add(new DateItem()
                {
                    day = namaazList[i-1],
                    month = "b",
                    dayWeek = "a",
                    selected = i == DateTime.Today.Day,
                    backgroundColor = i == DateTime.Today.Day ? "#FCCD00" : "Transparent",
                    textColor = i == DateTime.Today.Day ? "#000000" : "#FFFFFF",
                });
            }
        }

        private void ExecuteSelectDateCommand(DateItem model)
        {
            Dates.ToList().ForEach((item) =>
            {
                item.selected = false;
                item.backgroundColor = "Transparent";
                item.textColor = "#FFFFFF";
            });



            var index = Dates.ToList().FindIndex(p => p.day == model.day && p.dayWeek == model.dayWeek);
            if (index > -1)
            {
                Dates[index].backgroundColor = "#FCCD00";
                Dates[index].textColor = "#000000";
                Dates[index].selected = true;
            }
        }

        private void ExecuteSelectEventTypeCommand(EventType model)
        {
            EventTypes.ToList().ForEach((item) =>
            {
                item.selected = false;
                item.backgroundColor = "#29404E";
                item.textColor = "#FFFFFF";
            });

            var index = EventTypes.ToList().FindIndex(p => p.name == model.name);
            if (index > -1)
            {
                EventTypes[index].backgroundColor = "#FCCD00";
                EventTypes[index].textColor = "#000000";
                EventTypes[index].selected = true;
            }
        }
    }
}
