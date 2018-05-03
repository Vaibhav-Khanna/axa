using System;
using System.Collections.Generic;
using AXA.Controls;
using AXA.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace AXA.Pages
{
    public partial class MasterPage : ContentPage
    {
        public MasterPage()
        {
            InitializeComponent();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var item = e.Item as MasterMenuItem;

            if (item != null)
            {
                var context = BindingContext as MasterPageModel;
                context.ItemSelected.Execute(item);
            }

            listView.SelectedItem = null;
        }

    }
}
