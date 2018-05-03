using System;
using System.Text.RegularExpressions;
using AXA.Renderers;
using Xamarin.Forms;

namespace AXA.Behaviours
{
    public class EmailEntryBehaviour : Behavior<CustomEntry>
    {
        public readonly BindableProperty BoxColorProperty =
            BindableProperty.Create(nameof(BoxColor), typeof(BoxView), typeof(EmailEntryBehaviour));

        public BoxView BoxColor
        {
            get => (BoxView)GetValue(BoxColorProperty);
            set => SetValue(BoxColorProperty, value);
        }

        protected override void OnAttachedTo(CustomEntry bindable)
		{
            bindable.TextChanged += Bindable_TextChanged;
            base.OnAttachedTo(bindable);

		}

        protected override void OnDetachingFrom(CustomEntry bindable)
		{
            bindable.TextChanged -= Bindable_TextChanged;
            base.OnDetachingFrom(bindable);
		}

        void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(BoxColor!=null)
            {
                if(string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    BoxColor.Color = Color.Red; 
                }
                else
                {
                    if ((sender as Entry).Keyboard == Keyboard.Email)
                    {
                        bool isEmail = Regex.IsMatch(e.NewTextValue, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

                        if (isEmail)
                        {
                            BoxColor.Color = Color.Green;
                        }
                        else
                        {
                            BoxColor.Color = Color.Red;
                        }
                    }
                    else
                    {
                        BoxColor.Color = Color.Green;
                    }
                } 
            }
        }

	}
}
