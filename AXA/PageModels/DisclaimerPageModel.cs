using System;
using AXA.Controls;
using Xamarin.Forms;

namespace AXA.PageModels
{
    public class DisclaimerPageModel : BasePageModel
    {
        public bool IsAccept { get; set; }

        bool a1;
        public bool AgreeFirstTerm { get { return a1; } set{ a1 = value; Validate(); } }

        bool a2;
        public bool AgreeSecondTerm { get { return a2; } set{ a2 = value; Validate(); } }

        void Validate()
        {
            if (AgreeFirstTerm && AgreeSecondTerm)
            {
                IsAccept = true;
            }
            else
                IsAccept = false;
        }

        public Command AcceptCommand => new Command(async () =>
        {
            Application.Current.Properties["DisclaimerApproved"] = true;
            Application.Current.Properties["DisclaimerApprovedDate"] = DateTime.Now;

            await Application.Current.SavePropertiesAsync();

            Device.BeginInvokeOnMainThread(() =>
            {
                Application.Current.MainPage = new MasterDetailNavigationPage();
            });
        });
    }
}
