using System;
using AXA.Models;
using Xamarin.Forms;
using Plugin.Share;
using System.Threading.Tasks;
using System.Linq;
using Plugin.Connectivity;

namespace AXA.PageModels
{
    public class PublicationDetailPageModel : BasePageModel
    {

        public PublicationModel SelectedItem { get; set; }
        public string Text1 { get; set; } = "Download";
        public string Text2 { get; set; } = "Preview";
        public bool IsEnabled { get; set; } = true;
        byte[] PDF;

        public async override void Init(object initData)
        {
            base.Init(initData);

            if (initData is PublicationModel)
            {
                SelectedItem = ((PublicationModel)initData);

                var pdf = await StoreManager.PublicationStore.FetchPublication(SelectedItem.publication);

                if (pdf != null && pdf.Any())
                {
                    PDF = pdf;

                    Text1 = "Read";
                    Text2 = "Archive";
                }
            }
        }

        public Command ReadDownloadCommand => new Command(async () =>
        {
            if (Text1 == "Download")
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await CoreMethods.DisplayAlert("Not Connected", "You need to have an active internet connection to do this. If you are online and see this, try restart the application.", "Ok");
                    return;
                }

                // Download 
                IsEnabled = false;

                var pdf = await StoreManager.PublicationStore.DownloadPublication(SelectedItem.publication);

                IsEnabled = true;
                //

                if (pdf != null && pdf.Any())
                {
                    PDF = pdf;
                    Text1 = "Read";
                    Text2 = "Archive";
                }
            }
            else
            {
                await CoreMethods.PushPageModel<PdfViewerPageModel>(PDF, false, false);
            }
        });

        public Command ArchivePreviewCommand => new Command(async () =>
        {
            if (Text2 == "Archive")
            {
                // Archive or delete
                IsEnabled = false;

                var isArchived = await StoreManager.PublicationStore.ArchivePublication(SelectedItem.publication);

                IsEnabled = true;
                //

                if (isArchived)
                {
                    Text1 = "Download";
                    Text2 = "Preview";
                    await CoreMethods.PopPageModel();
                }
            }
            else
            {
                if(!CrossConnectivity.Current.IsConnected)
                {
                    await CoreMethods.DisplayAlert("Not Connected", "You need to have an active internet connection to do this. If you are online and see this, try restart the application.", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(SelectedItem.publication.ExcerptPages))
                {
                    await CoreMethods.DisplayAlert("Not Available", "Preview is not available for this item.", "Ok");
                    return;
                }

                // Download Preview
                IsEnabled = false;

                var pdf = await StoreManager.PublicationStore.DownloadPublication(SelectedItem.publication, true);

                IsEnabled = true;
                //

                int startNumber = 2;

                var numbers = SelectedItem.publication.ExcerptPages.Split(',').Select((arg) => Convert.ToInt32(arg.Trim())).OrderByDescending((arg) => arg);

                startNumber = numbers.First();

                await CoreMethods.PushPageModel<PdfViewerPageModel>(new Tuple<byte[],int>(pdf,startNumber), false, false);
            }
        });

        public Command ShareCommand => new Command(async () =>
        {
            await CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage()
            {
                Text = SelectedItem.Title + Environment.NewLine + (string.IsNullOrWhiteSpace(SelectedItem.Description) ? "" : "By " + SelectedItem.Description),
                Title = SelectedItem.Title,
                Url = $"http://admin.axa-im-insight.com/viewer/{SelectedItem.publication.Id}/0?referer=http://admin.axa-im-insight.com"        
            });
        });

    }
}
