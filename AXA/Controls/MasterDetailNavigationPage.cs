using System;
using FreshMvvm;
using Xamarin.Forms;
using AXA.PageModels;
using System.Linq;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Plugin.Connectivity;

namespace AXA.Controls
{
    public class MasterDetailNavigationPage : FreshMasterDetailNavigationContainer
    {

        AONNavigationContainer Detail_navigation;

        public MasterDetailNavigationPage()
        {
            Xamarin.Forms.Page detailpage;

            if (CrossConnectivity.Current.IsConnected)
            {
                detailpage = FreshPageModelResolver.ResolvePageModel<PublicationsListPageModel>(new MasterMenuItem() { Title = "All the latest AXA IM publications" });
            }
            else
            {
                detailpage = FreshPageModelResolver.ResolvePageModel<DownloadsPageModel>();
            }


            var masterpage = FreshPageModelResolver.ResolvePageModel<MasterPageModel>();

            var context = masterpage.BindingContext as MasterPageModel;

            if (context != null)
            {
                context.MenuItemSelected -= Context_MenuItemSelected;
                context.MenuItemSelected += Context_MenuItemSelected;
            }

            #region Setup

            masterpage.Title = " ";

            masterpage.Icon = "menu.png";

            MasterBehavior = MasterBehavior.Popover;

            IsGestureEnabled = true;

            if(Device.RuntimePlatform == Device.iOS)
            {
                if (Device.Idiom == TargetIdiom.Phone)
                {
                    IsGestureEnabled = true;
                }
            }          

            #endregion


            Detail_navigation = new AONNavigationContainer(detailpage) { BarTextColor = Color.White, BarBackgroundColor = Color.FromRgb(27, 63, 142) };

            Detail_navigation.On<Xamarin.Forms.PlatformConfiguration.iOS>().SetPrefersLargeTitles(true);

            Master = masterpage;

            Detail = Detail_navigation;
        }


        async void Context_MenuItemSelected(MasterMenuEventArgs args)
        {

            var currenPage = Detail_navigation.CurrentPage;

            var current_model = currenPage.BindingContext as FreshBasePageModel;

            if(args.item.TagetType == currenPage.GetType() && currenPage.Title == args.item.Title)
            {
                IsPresented = false;
                return;
            }


            switch (args.item.TagetType.ToString().Split('.').Last())
            {
                case "DownloadsPage":
                    {
                        await current_model.CoreMethods.PushPageModel<DownloadsPageModel>(null, false, false);
						Detail_navigation.Navigation.RemovePage(currenPage);
                        break;
                    }
                case "PublicationsListPage":
                    {
                        await current_model.CoreMethods.PushPageModel<PublicationsListPageModel>(args.item,false, false);
                        Detail_navigation.Navigation.RemovePage(currenPage);
                        break;
                    }
                case "AboutPage":
                    {
                        await current_model.CoreMethods.PushPageModel<AboutPageModel>(null,false, false);
                        Detail_navigation.Navigation.RemovePage(currenPage);
						break;
                    }
                case "LoginPage":
                    {
                        if((bool)App.Current.Properties["IsLoggedIn"]==true)
                        {
                            await current_model.CoreMethods.PushPageModel<SubscriptionDetailPageModel>(null, false, false);
                            Detail_navigation.Navigation.RemovePage(currenPage);
                        }
                        else
                        {
                            await current_model.CoreMethods.PushPageModel<SubscriptionPageModel>(null, false, false);
                            Detail_navigation.Navigation.RemovePage(currenPage);
                        }
                        break;
                    }
                default:
                    {
                        IsPresented = false;
                        return;
                    }
            }
           
            IsPresented = false;

        }
    }
}

