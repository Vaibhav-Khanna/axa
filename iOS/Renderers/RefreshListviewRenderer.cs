using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using AXA.iOS.Renderers;

[assembly: ExportRenderer(typeof(ListView), typeof(RefreshListviewRenderer))]
namespace AXA.iOS.Renderers
{
    public class RefreshListviewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            var vc = ((UITableViewController)ViewController);

            // Set the color!
            //if (vc?.RefreshControl != null)
                //vc.RefreshControl.TintColor = UIColor.FromRGB(255,255,255);
        }
    }
}
