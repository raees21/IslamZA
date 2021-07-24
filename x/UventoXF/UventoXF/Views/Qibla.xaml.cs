using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace UventoXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page2 : ContentPage
    {
        double current_latitude;
        double current_longitude;
        double QiblaLatitude = 21.4224779;
        double QiblaLongitude = 39.8251832;
        SensorSpeed speed = SensorSpeed.UI;

        public Page2()
        {
            InitializeComponent();
            BindingContext = this;
            SetLayoutFrame();
            GetLocation();
            PointToQibla();
            Compass.ReadingChanged += Compass_ReadingChanged;
        }

        private void SetLayoutFrame()
        {
            gridFrames.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
            gridFrames.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            gridFrames.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
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

        void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {
            circularGauge.RotateTo(360 - e.Reading.HeadingMagneticNorth);
            PointToQibla();
        }

        private void Scale_LabelCreated(object sender, Syncfusion.SfGauge.XForms.LabelCreatedEventArgs args)
        {
            switch ((string)args.LabelContent)
            {
                case "0":
                    args.LabelContent = "N";
                    break;
                case "45":
                    args.LabelContent = "NE";
                    break;
                case "90":
                    args.LabelContent = "E";
                    break;
                case "135":
                    args.LabelContent = "SE";
                    break;
                case "180":
                    args.LabelContent = "S";
                    break;
                case "225":
                    args.LabelContent = "SW";
                    break;
                case "270":
                    args.LabelContent = "W";
                    break;
                case "315":
                    args.LabelContent = "NW";
                    break;
                case "360":
                    args.LabelContent = "N";
                    break;
            }
        }

        private async void GetLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);
            Console.WriteLine(location.Latitude);
            current_latitude = location.Latitude;
            current_longitude = location.Longitude;
        }

        private double Mod(double a, double b)
        {
            return a - b * Math.Floor(a / b);
        }

        void PointToQibla()
        {
            double latt_from_radians = current_latitude * Math.PI / 180;
            double long_from_radians = current_longitude * Math.PI / 180;
            double latt_to_radians = QiblaLatitude * Math.PI / 180;
            double lang_to_radians = QiblaLongitude * Math.PI / 180;
            double bearing = Math.Atan2(Math.Sin(lang_to_radians - long_from_radians) * Math.Cos(latt_to_radians), (Math.Cos(latt_from_radians) * Math.Sin(latt_to_radians)) - (Math.Sin(latt_from_radians) * Math.Cos(latt_to_radians) * Math.Cos(lang_to_radians - long_from_radians)));
            bearing = Mod(bearing, 2 * Math.PI);
            double bearing_degree = bearing * 180 / Math.PI;
            pointer1.Value = bearing_degree;
        }
    }
}