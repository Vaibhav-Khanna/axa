using System;
using Xamarin.Forms;
using AXA.Helpers;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AXA.PageModels
{
    public class LoginPageModel : BasePageModel
    {
        [PropertyChanged.DoNotNotify]
        public string Username { get; set; }
        [PropertyChanged.DoNotNotify]
        public string Password { get; set; }


        public Command LoginCommand => new Command(async() =>
        {
            if (String.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Username))
            {
                await CoreMethods.DisplayAlert("Missing Information", "Please enter email and password to proceed.", "Ok");
                return;
            }

            bool isEmail = Regex.IsMatch(Username, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            if(!isEmail)
            {
                await CoreMethods.DisplayAlert("Uh oh !", "Please enter a valid email id to proceed.", "Ok");
                return;
            }

            Dialog.ShowLoading();

            var IsLoggedIn =  await StoreManager.LoginAsync(Username,Password);

            Dialog.HideLoading();

            if (IsLoggedIn)
            {
                await CoreMethods.PushPageModel<SubscriptionDetailPageModel>(false);

                CoreMethods.RemoveFromNavigation<SubscriptionPageModel>(true);

                CoreMethods.RemoveFromNavigation<LoginPageModel>(true);
            }
            else
            {
                await CoreMethods.DisplayAlert("Something is not right !", "Please check the email/password and retry again. You may also check your internet connection", "Ok");
            }

        });



    }
}
