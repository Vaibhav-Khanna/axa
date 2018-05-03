using System;
using System.Collections.ObjectModel;
using AXA.Controls;
using AXA.Pages;
using AXA.Templates;
using Xamarin.Forms;
using System.Linq;

namespace AXA.PageModels
{
    public class MasterPageModel : BasePageModel
    {
       
        public delegate void EventHandler(MasterMenuEventArgs args);

        public event EventHandler MenuItemSelected;
         
        public ObservableCollection<GroupedMasterModel> Items { get; set; }
         
        public string HeaderImage { get; set; }


        public Command ItemSelected => new Command((obj) =>
        {
            var item = obj as MasterMenuItem;

            if (item == null)
                return;
            MenuItemSelected?.Invoke(new MasterMenuEventArgs(item));
        });


        public async override void Init(object initData)
		{
            base.Init(initData);

            HeaderImage = "Logo.jpg";

            var config = await StoreManager.ConfigurationStore.GetConfiguration();

            if(config != null)
            {
                var _items = new ObservableCollection<GroupedMasterModel>();

                var menus = config?.Menus?.FirstOrDefault()?.Menu;

                GroupedMasterModel group = new GroupedMasterModel();

                for (int i = 0; i < menus.Count(); i++)
                {
                    if (menus[i].Action == "label")
                    {
                        group = new GroupedMasterModel() { GroupTitle = menus[i].Label };
                        _items.Add(group);
                    }
                    else
                    {
                        Type type = typeof(PublicationsListPage);

                        switch (menus[i].Action)
                        {
                            case "publications":
                                type = typeof(PublicationsListPage);
                                break;
                            case "downloads":
                                type = typeof(DownloadsPage);
                                break;
                            case "subscriber":
                                type = typeof(LoginPage);
                                break;
                            case "legals":
                                type = typeof(AboutPage);
                                break;
                            default:
                                break;
                        }

                        if(menus[i].Active)
                        group.Add(new MasterMenuItem() { Title = menus[i].Label, IsPublication = menus[i].Action == "publications", Color = "#27498c", TagetType = type, PublicationId = menus[i].Option });
                    }
                }

                Items = _items;
            }

            Update();
		}

        async void Update()
        {
            await StoreManager.ConfigurationStore.GetConfiguration(true);
        }

	}
}
