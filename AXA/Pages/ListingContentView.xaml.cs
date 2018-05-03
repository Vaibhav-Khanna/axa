using System;
using System.Collections.Generic;
using AXA.Models;
using FFImageLoading.Forms;
using Xamarin.Forms;
using System.Linq;

namespace AXA.Pages
{
    public partial class ListingContentView : ContentView
    {
        public ListingContentView()
        {
            InitializeComponent();
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            list.SelectedItem = null;
        }

        void Handle_BindingContextChanged(object sender, System.EventArgs e)
        {
            if((sender as ViewCell).BindingContext is PublicationModel)
            {
                (sender as ViewCell).View.FindByName<CachedImage>("image").Source = ((PublicationModel)(sender as ViewCell).BindingContext).Image;
            }
        }

        public void FocusSearch()
        {
            search.Text = string.Empty; 

            Device.BeginInvokeOnMainThread(() =>
            {
                if (!search.IsFocused)
                {
                    search.Focus();
                }
                else
                {
                    search.Unfocus();
                }
            });

            list.ScrollTo(search, ScrollToPosition.Center, false);
        }

    }
}
