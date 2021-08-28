using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Xml.Linq;
using UventoXF.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UventoXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {

        //double lblLatitude;
        //double lblLongitude;
        CancellationTokenSource cts;
        public double current_latitude { get; set; }
        public double current_longitude { get; set; }
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(Navigation);
            SetLayoutFrame();
            //getLocation();
        }

        private async void NavigateButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page1());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        //async void getLocation()
        //{
        //    try
        //    {
        //        var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
        //        cts = new CancellationTokenSource();
        //        var location = await Geolocation.GetLocationAsync(request, cts.Token);
        //        ((MainPageViewModel)BindingContext).current_latitude = location.Latitude;
        //        Console.WriteLine(((MainPageViewModel)BindingContext).current_latitude);
        //        ((MainPageViewModel)BindingContext).current_longitude = location.Longitude;
        //    }
        //    catch (FeatureNotSupportedException fnsEx)
        //    {
        //        await DisplayAlert("Faild", fnsEx.Message, "OK");
        //    }
        //    catch (PermissionException pEx)
        //    {
        //        await DisplayAlert("Faild", pEx.Message, "OK");
        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert("Faild", ex.Message, "OK");
        //    }
        //}

        async void btnLocation_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);
                Lat.Text = "Latitude:\n" + location.Latitude.ToString("0.00");
                Long.Text = "Longitude:\n" + location.Longitude.ToString("0.00");

                current_latitude = location.Latitude;
                current_longitude = location.Longitude;

                int indexVal = 0;
                var xml = XDocument.Load("http://api.timezonedb.com/v2.1/get-time-zone?key=HITW6BYBQTCK&format=xml&by=position&lat=" + current_latitude + "&lng=" + current_longitude);

                string currentDate = xml.Root.LastNode.ToString();

                if (currentDate.Length == 42)
                {
                    currentDate = xml.Root.LastNode.ToString().Substring(11, 10);
                    currentDate = currentDate.Substring(8, 2) + "-" + currentDate.Substring(5, 2) + "-" + currentDate.Substring(0, 4);
                    currentDate = currentDate.Trim();
                    string date = currentDate.Substring(0, 2);
                    string month = currentDate.Substring(3, 2);
                    string year = currentDate.Substring(6, 4);


                    var url = "https://api.aladhan.com/v1/calendar?latitude=" + current_latitude + "&longitude=" + current_longitude + "&method=1&month=" + month + "7&year=" + year;
                    var timings = _download_serialized_json_data<Rootobject>(url);

                    for (int i = 0; i < timings.data.Length; i++)
                    {
                        if (currentDate.Equals(timings.data[i].date.gregorian.date))
                        {
                            indexVal = i;
                        }
                    }
                    var namaazTime = new List<string>();

                    namaazTime.Add(timings.data[indexVal].timings.Fajr.Substring(0, 5));
                    namaazTime.Add(timings.data[indexVal].timings.Dhuhr.Substring(0, 5));
                    namaazTime.Add(timings.data[indexVal].timings.Asr.Substring(0, 5));
                    namaazTime.Add(timings.data[indexVal].timings.Maghrib.Substring(0, 5));
                    namaazTime.Add(timings.data[indexVal].timings.Isha.Substring(0, 5));

                    FajrTime.Text = timings.data[indexVal].timings.Fajr.Substring(0, 5);
                    ZohrTime.Text = timings.data[indexVal].timings.Dhuhr.Substring(0, 5);
                    AsrTime.Text = timings.data[indexVal].timings.Asr.Substring(0, 5);
                    MaghribTime.Text = timings.data[indexVal].timings.Maghrib.Substring(0, 5);
                    EshaTime.Text = timings.data[indexVal].timings.Isha.Substring(0, 5);
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Faild", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Faild", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Faild", ex.Message, "OK");
            }



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

        protected override void OnDisappearing()
        {
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
            base.OnDisappearing();
        }

        private void SetLayoutFrame()
        {
            //gridFrames.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
            //gridFrames.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            //gridFrames.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            frameHome.Padding = new Thickness(22, 5);
            frameHome.BackgroundColor = Color.FromHex("#102733");
            lbHome.IsVisible = true;
            frameSearch.Padding = new Thickness(0);
            frameSearch.BackgroundColor = Color.Transparent;
            lbSearch.IsVisible = false;
            lbSearch.TextColor = Color.White;
            lbIconSearch.TextColor = Color.White;
            frameFavorite.Padding = new Thickness(0);
            frameFavorite.BackgroundColor = Color.Transparent;
            lbFavorite.IsVisible = false;
            lbFavorite.TextColor = Color.White;
            lbIconFavorite.TextColor = Color.White;
        }
    }
}