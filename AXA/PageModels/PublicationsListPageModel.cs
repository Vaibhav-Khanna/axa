﻿using System;
using System.Collections.ObjectModel;
using AXA.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Plugin.Share;
using AXA.Helpers;
using System.Text.RegularExpressions;
using AXA.Controls;
using AXA.Models.DataObjects;
using Plugin.Connectivity;

namespace AXA.PageModels
{
    public class PublicationsListPageModel : BasePageModel
    {
        public string Title { get; set; }
        public ObservableCollection<PublicationModel> AllItemsSource { get; set; }
        public ObservableCollection<PublicationModel> ItemsSource { get; set; }
        public bool IsSelected { get; set; } = false;
		public bool IsLikeEnabled { get; set; } = true;
        public bool IsRefreshEnabled { get; set; } = true;
		public bool CanArchive { get; set; }
        public bool IsEnabled { get; set; } = true;
        string publicationId;
        byte[] PDF;
		List<Tuple<string, string>> CategoryData { get; set; }


        [PropertyChanged.DoNotNotifyAttribute]
        string _query;

        [PropertyChanged.DoNotNotifyAttribute]
        public string SearchQuery { get { return _query; } set { _query = value; Search(); RaisePropertyChanged(); } }

        PublicationModel item;
        public PublicationModel SelectedItem
        {
            get
            {
                return item;
            }
            set
            {
                if (value != null)
                {
                    item = value;

                    if (Device.Idiom == TargetIdiom.Tablet)
                    {
                        IsSelected = true;
                        FetchDocument();
                    }
                }
                else
                {
                    if (Device.Idiom == TargetIdiom.Tablet && item!=null)
                    {
                        IsSelected = true;
                        FetchDocument();
                    }
                    else
                    PublicationDetailCommand.Execute(null);
                }
            }
        }


		public async override void Init(object initData)
		{
			base.Init(initData);

			if (initData is MasterMenuItem)
			{
				Title = ((MasterMenuItem)initData).Title;

				IsLoading = true;

				IEnumerable<Publication> data;

				if (string.IsNullOrEmpty(((MasterMenuItem)initData).PublicationId))
					data = await StoreManager.PublicationStore.GetItemsAsync();
				else
					data = await StoreManager.PublicationStore.GetPublicationsByCategoryId(publicationId = ((MasterMenuItem)initData).PublicationId);

				CategoryData = await StoreManager.ConfigurationStore.GetCategories();

				ItemsSource = new ObservableCollection<PublicationModel>();

				var accountId = Application.Current.Properties["AccountID"];

				if (data != null && data.Any())
					for (int i = 0; i < data.Count(); i++)
					{
						var x = data.ElementAt(i);
						string category = "";

						if (CategoryData != null && CategoryData.Any() && !string.IsNullOrEmpty(x.Category))
						{
							category = CategoryData.Where((arg) => arg.Item1 == x.Category)?.First()?.Item2;
						}

						ItemsSource.Add(new PublicationModel()
						{
							Title = x.Title,
							LikeCount = x.CountLike,
							Description = category + (string.IsNullOrEmpty(x.Author) ? " " : " by ") + x.Author,
							Date = x.PublishedAt.DateTime,
							publication = x,
							BackColor = i % 2 == 0 ? "#eff2f3" : "#ffffff",
							PreviewText = (x.Description),
							Image = $"https://admin.axa-im-insight.com/kue/storage/{accountId}/documents/{x.Id}/pdf/thumbs/1.jpg"
						});
					}

				AllItemsSource = ItemsSource;

				IsLoading = false;
			}

			IsSelected = false;
		}

        void Search()
        {
            if (!string.IsNullOrWhiteSpace(SearchQuery) && ItemsSource.Any())
            {
                var query = SearchQuery.Trim().ToLower();

                ItemsSource = new ObservableCollection<PublicationModel>(AllItemsSource.Where((arg) => arg.Title.ToLower().Trim().Contains(query)));
            }
            else
                ItemsSource = AllItemsSource;
        }
  
        public Command RefreshCommand => new Command(async () =>
       {
           IEnumerable<Publication> data;

           if (string.IsNullOrEmpty(publicationId))
               data = await StoreManager.PublicationStore.GetItemsAsync(true);
           else
               data = await StoreManager.PublicationStore.GetPublicationsByCategoryId(publicationId, true);

			CategoryData = await StoreManager.ConfigurationStore.GetCategories();

		   if (data != null && data.Any())
		   {
			   ItemsSource = new ObservableCollection<PublicationModel>();

			   var accountId = Application.Current.Properties["AccountID"];

			   for (int i = 0; i < data.Count(); i++)
			   {
				   var x = data.ElementAt(i);
				   string category = "";

				   if (CategoryData != null && CategoryData.Any() && !string.IsNullOrEmpty(x.Category))
				   {
					   category = CategoryData.Where((arg) => arg.Item1 == x.Category)?.First()?.Item2;
				   }

				   ItemsSource.Add(new PublicationModel()
				   {
					   Title = x.Title,
				       LikeCount = x.CountLike,
					   Description = category + (string.IsNullOrEmpty(x.Author) ? " " : " by ") + x.Author,
					   Date = x.PublishedAt.DateTime,
					   publication = x,
					   BackColor = i % 2 == 0 ? "#eff2f3" : "#ffffff",
					   PreviewText = (x.Description),
					   Image = $"https://admin.axa-im-insight.com/kue/storage/{accountId}/documents/{x.Id}/pdf/thumbs/1.jpg"
				   });
			   }

			   AllItemsSource = ItemsSource;
		   }

           IsSelected = false;

           IsRefreshing = false;
       });

        async void FetchDocument()
        {
            var pdf = await StoreManager.PublicationStore.FetchPublication(SelectedItem.publication);

            if (pdf != null && pdf.Any())
            {
                PDF = pdf;

				CanArchive = true;
            }
            else
            {
                PDF = null;

				CanArchive = false;
            }
        }

        public Command PublicationDetailCommand => new Command(async () =>
        {
            if (Device.Idiom == TargetIdiom.Phone)
            {
                await CoreMethods.PushPageModel<PublicationDetailPageModel>(SelectedItem);
            }
        });

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
				if (PDF != null)
				{
					CanArchive = true;
					await CoreMethods.PushPageModel<PdfViewerPageModel>(PDF, false, false);
				}
                else
                {
					CanArchive = false;
                }
            }
        });

        public Command ArchivePreviewCommand => new Command(async () =>
        {
			if (PDF != null && CanArchive)
            {
				// Archive or delete
				Dialog.ShowLoading();

                IsEnabled = false;

                var isArchived = await StoreManager.PublicationStore.ArchivePublication(SelectedItem.publication);

                IsEnabled = true;
				//

				Dialog.HideLoading();

                if (isArchived)
                {
					PDF = null;
					CanArchive = false;
                }
            }
            else
            {
				PDF = null;
				CanArchive = false;
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
