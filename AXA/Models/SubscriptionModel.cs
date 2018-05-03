using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AXA.PageModels;
using Xamarin.Forms;

namespace AXA.Models
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class SubscriptionModel : PropertyNotifier
    {
        public SubscriptionModel(SubscriptionDetailPageModel pageModel,MenuMenu menu,bool sub,bool notif)
        {
            PageModel = pageModel;
            Menu = menu;

            issub = sub;
            isnotif = notif;

            RaisePropertyChanged("IsSubscribed");
            RaisePropertyChanged("IsNotified");
        }

        [PropertyChanged.DoNotNotify]
        public SubscriptionDetailPageModel PageModel { get; set; }

        [PropertyChanged.DoNotNotify]
        public MenuMenu Menu { get; set; }

        public string Color { get; set; } = "#dadada";

        public string Name { get; set; }

        [PropertyChanged.DoNotNotify]
        bool issub = true;
        [PropertyChanged.DoNotNotify]
        public bool IsSubscribed { get { return issub; } set { if(issub!=value){ PageModel.SubscriptionToggle.Execute(this); } issub = value; RaisePropertyChanged(); } }

        [PropertyChanged.DoNotNotify]
        bool isnotif=true;
        [PropertyChanged.DoNotNotify]
        public bool IsNotified { get { return isnotif; } set { if (isnotif != value) { PageModel.SubscriptionToggle.Execute(this); } isnotif = value; RaisePropertyChanged(); } }

        public bool IsSubEnabled { get; set; } = true;

        public bool IsNotifEnabled { get; set; } = true;

    }

    public class PropertyNotifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
       
        public void RaisePropertyChanged([CallerMemberName]string name = null)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        } 
    }
}
