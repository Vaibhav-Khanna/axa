using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akavache;
using AXA.DataStore.Abstraction.Stores;
using AXA.Models.DataObjects;
using System.Reactive.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Linq;

namespace AXA.DataStore.Implementation.Stores
{
    public class PublicationStore : BaseStore<Publication>, IPublicationStore
    {
        public async Task<bool> ArchivePublication(Publication publication)
        {
            try
            {
                var is_invalidate = await Storage.Invalidate("Down:" + publication.Id);
                return true;
            }
            catch(Exception)
            {
                
            }

            return false;
        }

        public async Task<byte[]> DownloadPublication(Publication publication,bool IsPreview = false)
        {
                     
            var uri = ($"https://admin.axa-im-insight.com/kue/storage/{Application.Current.Properties["AccountID"]}/documents/{publication.Id}/pdf/{publication.Id}.pdf");

            try
            {
                var _client = new HttpClient();

                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var bytes = await response.Content.ReadAsByteArrayAsync();

                    if (bytes != null && bytes.Any())
                    {
                        if(!IsPreview)
                        await Storage.Insert("Down:"+publication.Id, bytes, new TimeSpan(30, 0, 0, 0));

                        return bytes;
                    }
                }               
            }
            catch (Exception)
            {
                
            }

            return null;
        }

        public async Task<byte[]> FetchPublication(Publication publication)
        {
            try
            {
                var data = await Storage.Get("Down:" + publication.Id);
                return data;
            }
            catch(KeyNotFoundException)
            {
                
            }
            return null;
        }

        public async Task<IEnumerable<Publication>> GetDownloadedPublications()
        {
            try
            {
                var keys = await Storage.GetAllKeys();

                if (keys != null)
                {
                    var downloadedKeys = keys?.Where((arg) => arg.Contains("Down:"));

                    if (downloadedKeys != null && downloadedKeys.Any())
                    {
                        var downloadIds = downloadedKeys.Select((arg) => arg.Split(':')[1]);

                        var downloaded = await Storage.GetObjects<Publication>(downloadIds);

                        return downloaded.Select((arg) => arg.Value);
                    }
                }

            }
            catch(Exception)
            {
                
            }

            return null;
        }

        public async override Task<IEnumerable<Publication>> GetItemsAsync(bool refresh = false)
		{
            IEnumerable<Publication> items;

            try
            {
                items = await Storage.GetAllObjects<Publication>();
            }
            catch (KeyNotFoundException)
            {
                items = new List<Publication>();
            }

            if (refresh || !items.Any())
            {
                items = await DownloadAndSave();
            }

            return items.OrderByDescending((arg) => arg.PublishedAt);
		}

        public async Task<IEnumerable<Publication>> GetPublicationsByCategoryId(string publicationCategoryId, bool refresh = false)
        {
            IEnumerable<Publication> items;

            try
            {
                items = await Storage.GetAllObjects<Publication>();
            }
            catch (KeyNotFoundException)
            {
                items = new List<Publication>();
            }

            if (refresh || !items.Any())
            {
                items = await DownloadAndSave();
            }

            var query_items = items.Where((arg) => arg.Category == publicationCategoryId).OrderByDescending((arg) => arg.PublishedAt);

            return query_items;
        }

        async Task<IEnumerable<Publication>> DownloadAndSave()
        {
            try
            {
                var _client = new HttpClient();

                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Application.Current.Properties["Token"]);

                var uriService = new Uri(Constants.EndUrl + "/publications");

                var response = await _client.GetAsync(uriService);

                var string_content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var data = JsonConvert.DeserializeObject<List<Publication>>(string_content);

                    var keyvalueStore = new Dictionary<string, Publication>();

                    foreach (var item in data)
                    {
                        keyvalueStore.Add(item.Id, item);
                    }

                    if (data != null && data.Any())
                    {
                        await Storage.InvalidateAllObjects<Publication>();

                        var added = await Storage.InsertObjects<Publication>(keyvalueStore);

                        return data;
                    }
                }
            }
            catch (Exception)
            {
               
            }

            return new List<Publication>();
        }
	}
}
