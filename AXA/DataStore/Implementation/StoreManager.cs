using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AXA.DataStore.Abstraction;
using Newtonsoft.Json;
using AXA.Models;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Plugin.Connectivity;
using FreshMvvm;
using AXA.DataStore.Abstraction.Stores;
using XLabs.Cryptography;
using Akavache;
using System.Reactive.Linq;

namespace AXA.DataStore.Implementation
{
    public class StoreManager : IStoreManager
    {
       
        public StoreManager()
        {
               
        }

        IPublicationStore publicationStore;
        public IPublicationStore PublicationStore => publicationStore ?? (publicationStore = FreshIOC.Container.Resolve<IPublicationStore>());

        IConfigurationStore configurationStore;
        public IConfigurationStore ConfigurationStore => configurationStore ?? (configurationStore = FreshIOC.Container.Resolve<IConfigurationStore>());

        public async Task<bool> LoginAsync(string username, string password)
        {
            if (!CrossConnectivity.Current.IsConnected)
                return false;

            try
            {
                var _client = new HttpClient();
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Application.Current.Properties["Token"]);

                var uriService = new Uri(Constants.EndUrl + "/subscriber/valid/user");
               
                var credentials = new JObject();
                credentials["deviceId"] = ConvertToUnixTimestamp(DateTime.UtcNow);
                credentials["email"] = username;
                credentials["password"] = MD5Hash(password);
                credentials["platform"] = "web";

                var content = new StringContent(credentials.ToString(), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(uriService, content);
                var string_content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var token_data = JsonConvert.DeserializeObject<LoginResponse>(string_content);

                    try
                    {
                        await BlobCache.UserAccount.InsertObject<LoginResponse>("LoginResponse", token_data);
                    }
                    catch(Exception)
                    {
                        
                    }

                    Application.Current.Properties["IsLoggedIn"] = true;

                    await Application.Current.SavePropertiesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> LogoutAsync()
        {
            try
            {
                await BlobCache.UserAccount.InvalidateObject<LoginResponse>("LoginResponse");
            }
            catch (Exception)
            {

            }

            Application.Current.Properties["IsLoggedIn"] = false;

            await Application.Current.SavePropertiesAsync();

            return true;
        }

       

        public async Task<bool> VerifyToken()
        {
            if (!CrossConnectivity.Current.IsConnected)
                return true;
            
            try
            {
                var _client = new HttpClient();

                var uriService = new Uri(Constants.EndUrl + "/token/for/webdomain");

                var credentials = new JObject();
                credentials["webDomain"] = "admin";
                credentials["context"] = "axa-im-insight.com";

                var content = new StringContent(credentials.ToString(), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(uriService, content);
                var string_content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var token_data = JsonConvert.DeserializeObject<TokenResponse.Welcome>(string_content);

                    Application.Current.Properties["Token"] = token_data.Token;
                    Application.Current.Properties["AccountID"] = token_data.Account.Id;

                    await Application.Current.SavePropertiesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception)
            {
                return false;
            }
        }

        public static long ConvertToUnixTimestamp(DateTime date)
        {
            //DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return (long)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds; ;
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public async Task<bool> RegisterAsync(string email, string password, string company, string firstname, string lastname)
        {
            if (!CrossConnectivity.Current.IsConnected)
                return false;

            try
            {
                var _client = new HttpClient();
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Application.Current.Properties["Token"]);

                var uriService = new Uri(Constants.EndUrl + "/subscriber");

                var credentials = new JObject();
                credentials["createdAt"] = DateTime.Now;
                credentials["firstName"] = firstname;
                credentials["lastName"] = lastname;
                credentials["company"] = company;
                credentials["email"] = email;
                credentials["language"] = "en_US";
                credentials["password"] = MD5Hash(password);
                credentials["insertKind"] = 3;
                credentials["status"] = 1;

                var content = new StringContent(credentials.ToString(), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(uriService, content);
                var string_content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var isloggedin =  await LoginAsync(email, password);

                    return isloggedin;
                }
               
            }
            catch (Exception)
            {
               
            }

            return false;
        }
    }
}
