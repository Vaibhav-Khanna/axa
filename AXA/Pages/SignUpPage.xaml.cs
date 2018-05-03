using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace AXA.Pages
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();

            if (Device.Idiom == TargetIdiom.Phone)
            {
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetLargeTitleDisplay(LargeTitleDisplayMode.Never);
            }

            firstname.ReturnButton = Renderers.ReturnButtonType.Next;
            lastname.ReturnButton = Renderers.ReturnButtonType.Next;
            company.ReturnButton = Renderers.ReturnButtonType.Next;
            email.ReturnButton = Renderers.ReturnButtonType.Next;
            password.ReturnButton = Renderers.ReturnButtonType.Next;
            confpassword.ReturnButton = Renderers.ReturnButtonType.Done;

            firstname.NextView = lastname;
            lastname.NextView = company;
            company.NextView = email;
            email.NextView = password;
            password.NextView = confpassword;
            confpassword.DoneView = btlogin;
        }
    }
}
