﻿using Xamarin.Forms;
using AXA.Controls;
using AXA.PageModels;
using System;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using AXA.DataStore.Abstraction;
using FreshMvvm;

namespace AXA
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            BasePageModel.InitApp();

            (FreshIOC.Container.Resolve<IStoreManager>()).VerifyToken();

            if (((bool)Properties["DisclaimerApproved"]) == true && DateTime.Now.Subtract(((DateTime)Properties["DisclaimerApprovedDate"])).TotalDays <= 90)
            {
                MainPage = new MasterDetailNavigationPage();
            }
            else
            {
                var page = FreshMvvm.FreshPageModelResolver.ResolvePageModel<DisclaimerPageModel>();

                var navigationContainer = new AONNavigationContainer(page);

                navigationContainer.On<Xamarin.Forms.PlatformConfiguration.iOS>().SetPrefersLargeTitles(true);

                MainPage = navigationContainer;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}
