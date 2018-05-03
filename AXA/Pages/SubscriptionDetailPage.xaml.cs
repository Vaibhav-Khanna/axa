using System;
using System.Collections.Generic;
using AXA.Models;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace AXA.Pages
{
    public partial class SubscriptionDetailPage : ContentPage
    {
        public SubscriptionDetailPage()
        {
            InitializeComponent();

            if (Device.Idiom == TargetIdiom.Phone)
            {
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetLargeTitleDisplay(LargeTitleDisplayMode.Never);
            }
            else
            {
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetLargeTitleDisplay(LargeTitleDisplayMode.Always);
            }
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            list.SelectedItem = null;
        }

        void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            ((sender as Switch).BindingContext as SubscriptionModel).PageModel.SubscriptionToggle.Execute(((sender as Switch).BindingContext as SubscriptionModel));
        }
    }
}
