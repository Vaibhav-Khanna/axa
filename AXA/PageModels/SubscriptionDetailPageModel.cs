using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AXA.Models;
using Xamarin.Forms;
using AXA.Helpers;

namespace AXA.PageModels
{
    public class SubscriptionDetailPageModel : BasePageModel
    {
        public ObservableCollection<SubscriptionModel> Items { get; set; }

        public async override void Init(object initData)
        {
            base.Init(initData);

            Dialog.ShowLoading();

            var items = await StoreManager.ConfigurationStore.GetSubscriptionList();

            Items = new ObservableCollection<SubscriptionModel>();

            foreach (var item in items)
            {
                if(item.Item1.Active)
                    Items.Add(new SubscriptionModel(this,item.Item1,item.Item2,item.Item3) { Name = item.Item1.Label });
            }

            Dialog.HideLoading();
        }

        public Command SubscriptionToggle => new Command(async (SubscriptionModel) =>
        {
            Dialog.ShowLoading();

            var response = await StoreManager.ConfigurationStore.GetLoginResponse();

            if (response != null)
            {
                response.CategoriesBlocked = new List<string>();
                response.CategoriesNotifBlocked = new List<string>();

                foreach (var item in Items)
                {
                    if (!item.IsNotified)
                        response.CategoriesNotifBlocked.Add(item.Menu.Option);

                    if (!item.IsSubscribed)
                        response.CategoriesBlocked.Add(item.Menu.Option);
                }

                var isUpdated = await StoreManager.ConfigurationStore.UpdateSubscriptionDetail(response);

                var items = await StoreManager.ConfigurationStore.GetSubscriptionList();

                Items = new ObservableCollection<SubscriptionModel>();

                foreach (var item in items)
                {
                    if (item.Item1.Active)
                        Items.Add(new SubscriptionModel(this, item.Item1, item.Item2, item.Item3) { Name = item.Item1.Label });
                }
            }

            Dialog.HideLoading();
        });

        public Command LogoutCommand => new Command(async() =>
        {
            var response = await CoreMethods.DisplayAlert("Sign out", "Do you really want to sign out ?", "Yes", "Cancel");

            if(response)
            {
                var isloggedOut = await StoreManager.LogoutAsync();

                if(isloggedOut)
                {
                    await CoreMethods.PushPageModel<SubscriptionPageModel>(false);

                    CoreMethods.RemoveFromNavigation<SubscriptionDetailPageModel>(true);
                }
            }
        });

	}
}
