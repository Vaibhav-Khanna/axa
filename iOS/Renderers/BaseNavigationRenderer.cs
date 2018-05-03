using System;
using System.Collections.Generic;
using System.Linq;
using AXA.iOS.Renderers;
using CoreGraphics;

using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(BaseNavigationRenderer))]
namespace AXA.iOS.Renderers
{
    public class BaseNavigationRenderer : NavigationRenderer
    {

        public bool viewAdded = false;

		public override void ViewDidLoad()
		{
            base.ViewDidLoad();

            ////NavigationBar.ShadowImage = new UIImage();
            ////NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);

            //NavigationBar.Translucent = true;

            ////if (NavigationBar != null && !viewAdded)
            ////{
            ////    var effect = UIBlurEffect.FromStyle(UIBlurEffectStyle.Light);
            ////    var visualEffectView = new UIVisualEffectView(effect);
            ////    var bounds = NavigationBar.Bounds;
            ////    bounds.Offset(0, -10);
            ////    bounds = bounds.Inset(0, -10);
            ////    visualEffectView.Frame = bounds;
            ////    visualEffectView.Tag = 74619;
            ////    NavigationBar.AddSubview(visualEffectView);
            ////    NavigationBar.SendSubviewToBack(visualEffectView);
            ////    viewAdded = true;
            ////}

		}

	}
}
