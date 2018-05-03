using System;
using AXA.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("AXA")]
[assembly: ExportEffect(typeof(MultiLineEffect), "MultiLineEffect")]
namespace AXA.iOS.Effects
{
    public class MultiLineEffect : PlatformEffect
    {
        
        protected override void OnAttached()
        {
            (Control as UILabel).Lines = 5;
        }

        protected override void OnDetached()
        {
           
        }
    }
}
