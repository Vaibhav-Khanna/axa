using System;
using System.Text.RegularExpressions;
using AXA.Helpers;
using Xamarin.Forms;

namespace AXA.PageModels
{
    public class SignUpPageModel : BasePageModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string ConfirmPassword { get; set; }
        public string Password { get; set; }


        public Command SignUpCommand => new Command(async() =>
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(ConfirmPassword) || string.IsNullOrWhiteSpace(Password) )
            {
                await CoreMethods.DisplayAlert("Missing Information", "Please enter all the mandatory fields to proceed.", "Ok");
                return;
            }

            bool isEmail = Regex.IsMatch(Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            if (!isEmail)
            {
                await CoreMethods.DisplayAlert("Uh oh !", "Please enter a valid email id to proceed.", "Ok");
                return;
            }

            if(Password!=ConfirmPassword)
            {
                await CoreMethods.DisplayAlert("Uh oh !", "Password and confirm password do not match.", "Ok");
                return;
            }

            Dialog.ShowLoading();

            var notOccupied = await StoreManager.ConfigurationStore.IsEmailAlreadyInUse(Email);

            if(!notOccupied)
            {
                Dialog.HideLoading();
                await CoreMethods.DisplayAlert("Uh oh !", "The email is already in use with some other account. Try consider log in instead", "Ok");
                return;
            }

            var isSuccess = await StoreManager.RegisterAsync(Email,Password,Company,FirstName,LastName);

            Dialog.HideLoading();

            if (isSuccess)
            {
                await CoreMethods.PushPageModel<SubscriptionDetailPageModel>(false);

                CoreMethods.RemoveFromNavigation<SubscriptionPageModel>(true);

                CoreMethods.RemoveFromNavigation<SignUpPageModel>(true);
            }
            else
            {
                await CoreMethods.DisplayAlert("Something is not right !", "Please check the email/password and retry again. You may also check your internet connection", "Ok");
            }

        });

    }
}
