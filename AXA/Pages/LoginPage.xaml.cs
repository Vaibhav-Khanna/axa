using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace AXA.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            if (Device.Idiom == TargetIdiom.Phone)
            {
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetLargeTitleDisplay(LargeTitleDisplayMode.Never);
            }

            email.NextView = password;
            email.ReturnButton = Renderers.ReturnButtonType.Next;

            password.DoneView = btlogin;
            password.ReturnButton = Renderers.ReturnButtonType.Done;
        }
    }
}
