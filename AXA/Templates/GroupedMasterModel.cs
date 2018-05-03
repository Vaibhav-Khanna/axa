using System;
using System.Collections.ObjectModel;
using AXA.Controls;

namespace AXA.Templates
{
    public class GroupedMasterModel : ObservableCollection<MasterMenuItem>
    {
        public string GroupTitle { get; set; }      
    }
}
