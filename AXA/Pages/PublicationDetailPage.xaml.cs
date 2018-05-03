using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace AXA.Pages
{
    public partial class PublicationDetailPage : ContentPage
    {
        public PublicationDetailPage()
        {
            InitializeComponent();


            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetLargeTitleDisplay(LargeTitleDisplayMode.Never);
        }
    }
}
