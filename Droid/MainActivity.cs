using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FFImageLoading.Forms.Droid;
using Messier16.Forms.Android.Controls;
using Syncfusion.SfPdfViewer.XForms.Droid;
using Akavache;
using Lottie.Forms.Droid;
using Xamarin.Forms;

namespace AXA.Droid
{
    [Activity(Label = "AXA IM Insight", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            CachedImageRenderer.Init(false);

            var s = new SfPdfDocumentViewRenderer();

            CheckboxRenderer.Init();

            AnimationViewRenderer.Init();

            BlobCache.ApplicationName = "AXA";

            if(Device.Idiom == TargetIdiom.Phone)
            {
                RequestedOrientation = ScreenOrientation.Portrait;
            }
            else 
            {
                RequestedOrientation = ScreenOrientation.Landscape;
            }

            LoadApplication(new App());
        }
    }
}
