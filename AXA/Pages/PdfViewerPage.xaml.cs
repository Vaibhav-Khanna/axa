using System;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;

namespace AXA.Pages
{
    public partial class PdfViewerPage : ContentPage
    {
        public PdfViewerPage()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
           
            InitializeComponent();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
	}
}
