using System;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;
using AXA.PageModels;

namespace AXA.Pages
{
    public partial class PublicationsListPage : ContentPage
    {
        ToolbarItem toolbar;

        public PublicationsListPage()
        {
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "Back");

            toolbar = new ToolbarItem() { Icon = "share.png", Text="Share" };
            toolbar.SetBinding(MenuItem.CommandProperty, "ShareCommand");

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetLargeTitleDisplay(LargeTitleDisplayMode.Never);
            }
            else
            {
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetLargeTitleDisplay(LargeTitleDisplayMode.Never); 
            }
        }

		protected override void OnBindingContextChanged()
		{
            base.OnBindingContextChanged();

            if(BindingContext is PublicationsListPageModel)
            {
                if (Device.Idiom == TargetIdiom.Tablet)
                {
                    ((PublicationsListPageModel)BindingContext).PropertyChanged -= Handle_PropertyChanged;
                    ((PublicationsListPageModel)BindingContext).PropertyChanged += Handle_PropertyChanged;
                }
            }
		}

        void Handle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                if (((PublicationsListPageModel)BindingContext).IsSelected)
                {
                    if (!ToolbarItems.Contains(toolbar))
                        ToolbarItems.Add(toolbar);
                }
                else
                {
                    if (ToolbarItems.Contains(toolbar))
                        ToolbarItems.Remove(toolbar);
                }
            }
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            listing.FocusSearch();
        }
	}
}
