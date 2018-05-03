using System;
using System.Collections.Generic;
using AXA.PageModels;
using Xamarin.Forms;

namespace AXA.Pages
{
    public partial class DownloadsPage : ContentPage
    {
        ToolbarItem toolbar;

        public DownloadsPage()
        {
            InitializeComponent();

            toolbar = new ToolbarItem() { Icon = "share.png", Text = "Share" };
            toolbar.SetBinding(MenuItem.CommandProperty, "ShareCommand");
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext is DownloadsPageModel)
            {
                if (Device.Idiom == TargetIdiom.Tablet)
                {
                    ((DownloadsPageModel)BindingContext).PropertyChanged -= Handle_PropertyChanged;
                    ((DownloadsPageModel)BindingContext).PropertyChanged += Handle_PropertyChanged;
                }
            }
        }

        void Handle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                if (((DownloadsPageModel)BindingContext).IsSelected)
                {
                    if (!ToolbarItems.Contains(toolbar))
                        ToolbarItems.Add(toolbar);
                }
                else
                {
                    if (ToolbarItems.Contains(toolbar))
                        ToolbarItems.Remove(toolbar);
                }
            }
        }
    }
}
