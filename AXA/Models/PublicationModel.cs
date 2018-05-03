using System;
using System.ComponentModel;
using AXA.Models.DataObjects;
using Xamarin.Forms;

namespace AXA.Models
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class PublicationModel
    {
        public Publication publication { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string PreviewText { get; set; }

        public string Image { get; set; }

        public string BackColor { get; set; }  
    }
}