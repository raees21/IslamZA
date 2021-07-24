using UventoXF.Interfaces;
using UventoXF.Views;
using Xamarin.Forms;

namespace UventoXF
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDc4ODU4QDMxMzkyZTMyMmUzMFQyRFMxRGVHRm04bVprZm55RFoyYXZOZHpKYTRCdWRRZnJPTHc3dURxM0U9");

            MainPage = new NavigationPage(new MainPage());

            if (Device.RuntimePlatform == Device.iOS)
                DependencyService.Get<IStatusbarColor>().ChangeStatusbarColor();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
