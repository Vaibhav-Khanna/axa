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

		public double? LikeCount { get; set; }

		public string Like { get { return ShowLike ? LikeCount.ToString() : ""; } }

		public bool ShowLike { get { return (LikeCount == null || LikeCount == 0) ? false : true; } }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string PreviewText { get; set; }

        public string Image { get; set; }

        public string BackColor { get; set; }  
    }
}