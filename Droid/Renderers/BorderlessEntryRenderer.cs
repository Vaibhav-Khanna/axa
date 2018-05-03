using System;
using Android.Content;
using AXA.Droid.Renderers;
using AXA.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(BorderlessEntryRenderer))]
namespace AXA.Droid.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        public BorderlessEntryRenderer(Context context) : base(context) 
        {
            
        } 

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null && e.NewElement!= null)
            {
                Control.Background = null;


                var element = e.NewElement as CustomEntry;
              
                if (element.ReturnButton == ReturnButtonType.Next)
                {
                    Control.ImeOptions = Android.Views.InputMethods.ImeAction.Next;
                    Control.EditorAction += (sender, args) =>
                    {
                        element.OnNext();
                    };
                }  
                else if (element.ReturnButton == ReturnButtonType.Done)
                {
                    Control.ImeOptions = Android.Views.InputMethods.ImeAction.Go;
                    //Control.InputType = Android.Text.InputTypes.TextVariationPassword;

                    Control.EditorAction += (sender, args) =>
                    {
                        element.OnDone();
                    };
                }
            }
        }
    }
}
