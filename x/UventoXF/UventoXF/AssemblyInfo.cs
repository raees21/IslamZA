using Android;
using Android.App;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: ExportFont("muli-resgular.ttf", Alias = "MuliRegular")]
[assembly: ExportFont("muli-bold.ttf", Alias = "MuliBold")]
[assembly: ExportFont("muli-black.ttf", Alias = "MuliBlack")]
[assembly: ExportFont("opensans-semibold.ttf", Alias = "OpenSansSemiBold")]
[assembly: ExportFont("fontello.ttf", Alias = "Fontello")]
[assembly: ExportFont("materialdesignicons.ttf", Alias = "MaterialIcons")]
[assembly: UsesPermission(Android.Manifest.Permission.AccessCoarseLocation)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessFineLocation)]
[assembly: UsesFeature("android.hardware.location", Required = false)]
[assembly: UsesFeature("android.hardware.location.gps", Required = false)]
[assembly: UsesFeature("android.hardware.location.network", Required = false)]
[assembly: UsesPermission(Manifest.Permission.AccessBackgroundLocation)]