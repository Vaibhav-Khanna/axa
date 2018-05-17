using System;
using System.Threading.Tasks;
using AXA.DataStore.Abstraction.Stores;
using AXA.Models;
using Akavache;
using System.Reactive.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Text;

namespace AXA.DataStore.Implementation.Stores
{
    public class ConfigurationStore : BaseStore<Configuration>, IConfigurationStore
    {
		public async Task<List<Tuple<string, string>>> GetCategories()
		{
			try
			{
				var config = await GetConfiguration();

				if (config != null)
				{
					List<Tuple<string, string>> list = new List<Tuple<string, string>>();

					foreach (var item in config?.Menus?.FirstOrDefault()?.Menu)
					{
						if (!string.IsNullOrEmpty(item.Option))
						{
							list.Add(new Tuple<string, string>(item.Option,item.Label));
						}
					}

					return list;
				}

			}
			catch(Exception)
			{
				
			}

			return null;
		}

		public async Task<Configuration> GetConfiguration(bool refresh = false)
        {
            Configuration item;

            try
            {
                item = await Storage.GetObject<Configuration>("AccountConfiguration");
            }
            catch (KeyNotFoundException)
            {
                item = null;
            }

            if (refresh || item == null)
            {
                try
                {
                    var _client = new HttpClient();

                    _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Application.Current.Properties["Token"]);

                    var uriService = new Uri(Constants.EndUrl + "/accountconfiguration");

                    var response = await _client.GetAsync(uriService);

                    var string_content = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var data = JsonConvert.DeserializeObject<List<Configuration>>(string_content);
                                             
                        if (data != null)
                        {
                            await Storage.InvalidateAllObjects<Configuration>();

                            var added = await Storage.InsertObject<Configuration>("AccountConfiguration",data.First());

                            item = data.First();
                        }
                    }
                }
                catch (Exception)
                {

                }
            }

            return item;
        }

        public async Task<LoginResponse> GetLoginResponse()
        {
            LoginResponse loginResponse;

            try
            {
                loginResponse = await Storage.GetObject<LoginResponse>("LoginResponse");
            }
            catch (KeyNotFoundException)
            {
                loginResponse = null;
            }

            return loginResponse;
        }

        public async Task<IEnumerable<Tuple<MenuMenu, bool,bool>>> GetSubscriptionList()
        {
            List<Tuple<MenuMenu, bool, bool>> data = new List<Tuple<MenuMenu, bool, bool>>();

            Configuration item;

            LoginResponse loginResponse;

            try
            {
                item = await Storage.GetObject<Configuration>("AccountConfiguration");
            }
            catch (KeyNotFoundException)
            {
                item = null;
            }


            try
            {
                loginResponse = await Storage.GetObject<LoginResponse>("LoginResponse");
            }
            catch (KeyNotFoundException)
            {
                loginResponse = null;
            }

            try
            {
                var _client = new HttpClient();

                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Application.Current.Properties["Token"]);

                var uriService = new Uri(Constants.EndUrl + "/subscriber/"+loginResponse?.Id);

                var response = await _client.GetAsync(uriService);

                var string_content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var newresponse = JsonConvert.DeserializeObject<LoginResponse>(string_content);

                    if (newresponse != null)
                    {
                        loginResponse = newresponse;

                        await Storage.InvalidateAllObjects<LoginResponse>();

                        await Storage.InsertObject<LoginResponse>("LoginResponse", newresponse);
                    }
                }
            }
            catch (Exception)
            {

            }


            try
            {
                if (item != null)
                {
                    var names = item.Menus.First().Menu.Where((arg) => arg.Action == "publications" && !string.IsNullOrEmpty(arg.Option));
                                     
                    if (loginResponse != null)
                        foreach (var name in names)
                        {
                            bool IsActive = true;
                            bool IsNotificationActive = true;

                            if (loginResponse.CategoriesBlocked != null && loginResponse.CategoriesBlocked.Any())
                            {
                                if (loginResponse.CategoriesBlocked.Contains(name.Option))
                                    IsActive = false;
                            }

                            if (loginResponse.CategoriesNotifBlocked != null && loginResponse.CategoriesNotifBlocked.Any())
                            {
                                if (loginResponse.CategoriesNotifBlocked.Contains(name.Option))
                                    IsNotificationActive = false;
                            }

                            var tuple = new Tuple<MenuMenu, bool, bool>(name, IsActive, IsNotificationActive);

                            data.Add(tuple);
                        }

                }
            }
            catch(Exception)
            {
                
            }

            return data;
        }

        public async Task<bool> IsEmailAlreadyInUse(string email)
        {
            try
            {
                var _client = new HttpClient();

                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Application.Current.Properties["Token"]);

                var uriService = new Uri(Constants.EndUrl + "/subscriber/email/already/used?email="+email);

                var response = await _client.GetAsync(uriService);

                var string_content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var exists = JsonConvert.DeserializeObject<EmailExists>(string_content);

                    return exists.Success;
                }
            }
            catch (Exception)
            {
                
            }

            return true;
        }

        public async Task<bool> UpdateSubscriptionDetail(LoginResponse response)
        {
            try
            {
                var _client = new HttpClient();
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Application.Current.Properties["Token"]);

                var uriService = new Uri(Constants.EndUrl + "/subscriber/"+response.Id);

                var credentials = JsonConvert.SerializeObject(response);

                var content = new StringContent(credentials, Encoding.UTF8, "application/json");
                var webresponse = await _client.PostAsync(uriService, content);
                var string_content = await webresponse.Content.ReadAsStringAsync();

                if (webresponse.IsSuccessStatusCode)
                {
                    var token_data = JsonConvert.DeserializeObject<LoginResponse>(string_content);

                    try
                    {
                        if (token_data != null)
                        {
                            await Storage.InvalidateObject<LoginResponse>("LoginResponse");
                            await Storage.InsertObject<LoginResponse>("LoginResponse", token_data);
                            return true;
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception)
            {
             
            }

            return false;
        }


        private class EmailExists
        {
            [JsonProperty("success")]
            public bool Success { get; set; }
        }
    }
}
