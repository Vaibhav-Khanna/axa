using System;
using FreshMvvm;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace AXA.Controls
{

    public class AONNavigationContainer : FreshNavigationContainer
    {
        public AONNavigationContainer(Xamarin.Forms.Page page) : base(page)
        {
            BarTextColor = Color.White;
            BarBackgroundColor = (Color)App.Current.Resources["primary"];
        }

        protected override Xamarin.Forms.Page CreateContainerPage(Xamarin.Forms.Page page)
        {
            if (page is Xamarin.Forms.NavigationPage || page is MasterDetailPage || page is TabbedPage)
                return page;

            return new AONNavigationPage(page);
        }
    }


    public class AONNavigationPage : Xamarin.Forms.NavigationPage
    {
        public AONNavigationPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetPrefersLargeTitles(true);
        }

        public AONNavigationPage(Xamarin.Forms.Page page) : base(page)
        {
            BarBackgroundColor = (Color)App.Current.Resources["primary"];
            BarTextColor = Color.White;


            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetPrefersLargeTitles(true);

        }
    }

}
