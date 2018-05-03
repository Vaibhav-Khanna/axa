using System;
using Xamarin.Forms;

namespace AXA.Renderers
{
    public class CustomEntry : Entry
    {
        public static readonly BindableProperty ReturnButtonProperty =
            BindableProperty.Create("ReturnButton", typeof(ReturnButtonType), typeof(CustomEntry), ReturnButtonType.None);

        public ReturnButtonType ReturnButton
        {
            get { return (ReturnButtonType)GetValue(ReturnButtonProperty); }
            set { SetValue(ReturnButtonProperty, value); }
        }

        public static readonly BindableProperty NextViewProperty =
            BindableProperty.Create("NextView", typeof(View), typeof(CustomEntry));

        public View NextView
        {
            get { return (View)GetValue(NextViewProperty); }
            set { SetValue(NextViewProperty, value); }
        }

        public void OnNext()
        {
            NextView?.Focus();
        }

        public static readonly BindableProperty DoneViewProperty =
            BindableProperty.Create("DoneView", typeof(View), typeof(CustomEntry));

        public View DoneView
        {
            get { return (View)GetValue(DoneViewProperty); }
            set { SetValue(DoneViewProperty, value); }
        }

        public void OnDone()
        {
            if (DoneView != null)
            {
                if (DoneView is Button)
                {
                    ((Button)DoneView).SendClicked();
                }
                else
                    DoneView?.Focus();
            }
        }

    }

    public enum ReturnButtonType
    {
        None, Done, Next
    }
}