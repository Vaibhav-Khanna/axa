using System;
using Rg.Plugins.Popup.Services;
using System.Linq;

namespace AXA.Helpers
{
    public static class Dialog
    {
        public static async void ShowLoading(string text = null)
        {
            var popupPage = new DialogLayout(text);

            if (PopupNavigation.PopupStack.Any())
            {
                await PopupNavigation.PopAllAsync();
            }

            await PopupNavigation.PushAsync(popupPage,false);
        }


        public static async void HideLoading()
        {
            if (PopupNavigation.PopupStack.Any())
            {
                await PopupNavigation.PopAllAsync();
            }
        }
    }
}
