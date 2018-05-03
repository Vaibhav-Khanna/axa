using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Forms.Touch;
using Foundation;
using Messier16.Forms.iOS.Controls;
using Syncfusion.SfPdfViewer.XForms.iOS;
using UIKit;
using Lottie.Forms.iOS;
using Akavache;
using Lottie.Forms.iOS.Renderers;

namespace AXA.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            CheckboxRenderer.Init();
           
            var s = new SfPdfDocumentViewRenderer();

            CachedImageRenderer.Init();

            AnimationViewRenderer.Init();

            BlobCache.ApplicationName = "AXA";

            LoadApplication(new App());

            var result = base.FinishedLaunching(app, options);

            UIApplication.SharedApplication.StatusBarHidden = false;

            return result;
        }
    }
}
