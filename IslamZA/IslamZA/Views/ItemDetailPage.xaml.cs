using IslamZA.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace IslamZA.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}