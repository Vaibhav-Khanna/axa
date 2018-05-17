using System;
using AXA.Renderers;
using AXA.iOS;
using AXA.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Foundation;
using System.ComponentModel;
using AXA.Controls;

[assembly: ExportRenderer(typeof(HtmlLabel), typeof(HTMLlabelRender))]
namespace AXA.iOS.Renderers
{
	public class HTMLlabelRender : LabelRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			if(Control!=null && Element!=null && !string.IsNullOrEmpty(Element.Text))
			{
				var attr = new NSAttributedStringDocumentAttributes();
                var nsError = new NSError();
                attr.DocumentType = NSDocumentType.HTML;

				var myHtmlData = NSData.FromString(Element?.Text, NSStringEncoding.Unicode);
                this.Control.AttributedText = new NSAttributedString(myHtmlData, attr, ref nsError);
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == Label.TextProperty.PropertyName)
			{
				var attr = new NSAttributedStringDocumentAttributes();
				var nsError = new NSError();        
				attr.DocumentType = NSDocumentType.HTML;

				var myHtmlData = NSData.FromString(Element.Text == null ? " " : Element.Text, NSStringEncoding.Unicode);
				this.Control.AttributedText = new NSAttributedString(myHtmlData, attr, ref nsError);
			}
		}

	}
}
