using System;
using AXA.PageModels;
using Xamarin.Forms;
using System.ComponentModel;

namespace AXA.Controls
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class MasterMenuItem
    {
        public string Title { get; set; }

        public bool IsPublication { get; set; }

        public string Color { get; set; } = "#ffffff";

        public Type TagetType { get; set; } = typeof(ContentPage);

        public string PublicationId { get; set; }
    }

	public class MasterMenuEventArgs : EventArgs
	{

		public MasterMenuItem item { get; internal set; }
	
        public MasterMenuEventArgs(MasterMenuItem _item)
		{
            this.item = _item;	
		}
	}

}
