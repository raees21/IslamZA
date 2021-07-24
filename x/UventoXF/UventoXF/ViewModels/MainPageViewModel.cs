using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using UventoXF.Helpers;
using UventoXF.Models;
using UventoXF.ViewModel;
using UventoXF.Views;
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
        public Command NavigateToTasbeehPageCommand { get; }
        public Command NavigateToQiblaPageCommand { get; }
        public Command NavigateTo99NamesPageCommand { get; }
        public Command SelectDateCommand { get; }
        public Command SelectEventTypeCommand { get; }
        public ObservableCollection<EventType> EventTypes { get; }
        public ObservableCollection<EventItem> EventItems { get; }
        public ObservableCollection<DateItem> Dates { get; }

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

        public void namaazCalculation()
        {
            Console.WriteLine("w");
        }

        private void loadDates()
        {


            var namaazList = new List<string>();

            namaazList.Add("Fajr");
            namaazList.Add("Zohr");
            namaazList.Add("Asr");
            namaazList.Add("Maghrib");
            namaazList.Add("Esha");

            for (int i = 1; i <= 5; i++)
            {
                Dates.Add(new DateItem()
                {
                    day = namaazList[i-1],
                    month = "b",
                    dayWeek = DateTime.Now.ToString().Substring(10,4),
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
