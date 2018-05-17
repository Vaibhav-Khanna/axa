using System;
using AXA.Models;
using Xamarin.Forms;
using Plugin.Share;
using System.Threading.Tasks;
using System.Linq;
using Plugin.Connectivity;
using AXA.Helpers;

namespace AXA.PageModels
{
    public class PublicationDetailPageModel : BasePageModel
    {

        public PublicationModel SelectedItem { get; set; }
		public bool CanArchive { get; set; }
		public bool IsLikeEnabled { get; set; } = true;
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
					CanArchive = true;
                }
            }
        }

        public Command ReadDownloadCommand => new Command(async () =>
        {
			if (PDF == null)
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await CoreMethods.DisplayAlert("Not Connected", "You need to have an active internet connection to do this. If you are online and see this, try restart the application.", "Ok");
                    return;
                }

				Dialog.ShowLoading();

                // Download 
                IsEnabled = false;

                var pdf = await StoreManager.PublicationStore.DownloadPublication(SelectedItem.publication);

                IsEnabled = true;
				//

				Dialog.HideLoading();

                if (pdf != null && pdf.Any())
                {
                    PDF = pdf;
					CanArchive = true;
					await CoreMethods.PushPageModel<PdfViewerPageModel>(PDF, false, false);
                }
            }
            else
            {
                await CoreMethods.PushPageModel<PdfViewerPageModel>(PDF, false, false);
            }
        });

        public Command ArchivePreviewCommand => new Command(async () =>
        {
			if (PDF!=null && CanArchive)
            {
                // Archive or delete
                IsEnabled = false;

                var isArchived = await StoreManager.PublicationStore.ArchivePublication(SelectedItem.publication);

                IsEnabled = true;
                //

                if (isArchived)
                {
					PDF = null;
					CanArchive = false;
                    await CoreMethods.PopPageModel();
                }
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

		public Command LikeCommand => new Command(async () =>
        {
            IsLikeEnabled = false;

            var liked = await StoreManager.PublicationStore.LikePublication(SelectedItem.publication.Id);

            IsLikeEnabled = true;

            if (liked)
            {
                if (SelectedItem.LikeCount == null)
                    SelectedItem.LikeCount = 0;

                SelectedItem.LikeCount += 1;

				SelectedItem.publication.CountLike = SelectedItem.LikeCount;

				await StoreManager.PublicationStore.SavePublication(SelectedItem.publication);
            }

        });
    }
}
