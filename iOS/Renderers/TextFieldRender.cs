using System;
using AXA.Renderers;
using AXA.iOS;
using AXA.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(TextFieldRender))]
namespace AXA.iOS.Renderers
{
    public class TextFieldRender : EntryRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null && Element!=null )
            {

                var label = (CustomEntry)Element;

                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;

                if (Control != null && e.NewElement != null)
                {
                    var element = e.NewElement as CustomEntry;

                    if (element.ReturnButton == ReturnButtonType.Next)
                    {
                        Control.ReturnKeyType = UIKit.UIReturnKeyType.Next;

                        Control.ShouldReturn += (textField) =>
                        {
                            element.OnNext();
                            return false;
                        };
                    }
                    else if (element.ReturnButton == ReturnButtonType.Done)
                    {
                        Control.ReturnKeyType = UIKit.UIReturnKeyType.Go;

                        Control.ShouldReturn += (textField) =>
                        {
                            element.OnDone();
                            return false;
                        };
                    }
                }

            }
        }
    }
}