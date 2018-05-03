using System;
using Android.Widget;
using AXA.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("AXA")]
[assembly: ExportEffect(typeof(MultiLineEffect), "MultiLineEffect")]
namespace AXA.Droid.Effects
{
    public class MultiLineEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            (Control as TextView)?.SetMaxLines(5);
        }

        protected override void OnDetached()
        {
           
        }
    }
}
