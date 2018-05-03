using System;
using Xamarin.Forms;

namespace AXA.PageModels
{
    public class SubscriptionPageModel : BasePageModel
    {
        
        public Command LoginCommand => new Command(async() =>
        {
            await CoreMethods.PushPageModel<LoginPageModel>();
        });

        public Command SignupCommand => new Command(async () =>
        {
            await CoreMethods.PushPageModel<SignUpPageModel>();
        });

    }
}
