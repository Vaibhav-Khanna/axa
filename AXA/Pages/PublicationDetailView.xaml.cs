using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AXA.Pages
{
    public partial class PublicationDetailView : ContentView
    {
        public PublicationDetailView()
        {
            InitializeComponent();



            if(Device.Idiom == TargetIdiom.Tablet)
            {
                Grid.SetRow(lbpreview, 0);
                Grid.SetColumn(lbpreview, 1);


                lbpreview.TranslationY = 70;
            }
            else
            {
                Grid.SetRow(lbpreview, 1);
                Grid.SetColumn(lbpreview, 0);
                Grid.SetColumnSpan(lbpreview, 2);

                lbpreview.TranslationY = 0;
            }

        }
    }
}
