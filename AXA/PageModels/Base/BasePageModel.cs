using System;
using AXA.DataStore.Abstraction;
using AXA.DataStore.Abstraction.Stores;
using AXA.DataStore.Implementation;
using AXA.DataStore.Implementation.Stores;
using FreshMvvm;
using Xamarin.Forms;

namespace AXA.PageModels
{
    public class BasePageModel : FreshBasePageModel
    {
        public bool IsRefreshing { get; set; }

        public bool IsLoading { get; set; }

        public bool IsLoadingText { get; set; }

        public async static void InitApp()
		{
            if (!Application.Current.Properties.ContainsKey("DisclaimerApproved"))
            {
                Application.Current.Properties.Add("DisclaimerApproved", false);
                Application.Current.Properties.Add("DisclaimerApprovedDate", DateTime.Now);
                Application.Current.Properties.Add("IsLoggedIn", false);
                Application.Current.Properties.Add("AccountID", string.Empty);
                Application.Current.Properties.Add("Token", string.Empty);
            }

            FreshIOC.Container.Register<IStoreManager, StoreManager>();
            FreshIOC.Container.Register<IPublicationStore, PublicationStore>();
            FreshIOC.Container.Register<IConfigurationStore, ConfigurationStore>();

            await Application.Current.SavePropertiesAsync();
		}

        protected IStoreManager StoreManager { get; } = FreshIOC.Container.Resolve<IStoreManager>();

	}
}
