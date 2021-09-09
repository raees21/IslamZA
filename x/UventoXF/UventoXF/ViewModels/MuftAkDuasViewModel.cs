using System;
using System.Collections.Generic;
using System.Text;
using UventoXF.ViewModel;
using Xamarin.Forms;

namespace UventoXF.ViewModels
{
    public class MuftAkDuasViewModel : BaseViewModel
    {
        public Command<object> OuterListTapCommand { get; set; }

        
        public MuftAkDuasViewModel()
        {
            OuterListTapCommand = new Command<object>(OnOuterListTapped);
        }

        private void OnOuterListTapped(object obj)
        {
            var item = obj as ContactInfo_NestedListView;
            item.IsInnerListVisible = !item.IsInnerListVisible;
        }
    }

    public class ContactInfo_NestedListView : BaseViewModel
    {
        private bool isInnerListVisible;

        public Command InnerListTapCommand { get; set; }

        public bool IsInnerListVisible
        {
            get { return isInnerListVisible; }
            set
            {
                isInnerListVisible = value;
                this.OnPropertyChanged("IsInnerListVisible");
            }
        }

        public ContactInfo_NestedListView(int CountMax)
        {
            InnerListTapCommand = new Command(OnInnerListTapped);
        }

        private void OnInnerListTapped(object obj)
        {
            var item = obj as DetailsContactInfo;
            item.IsSubListVisible = !item.IsSubListVisible;
        }
    }

    public class DetailsContactInfo : BaseViewModel
    {
        private bool isSubListVisible;

        public bool IsSubListVisible
        {
            get { return isSubListVisible; }
            set
            {
                isSubListVisible = value;
                this.OnPropertyChanged("IsSubListVisible");
            }
        }
    }
}
