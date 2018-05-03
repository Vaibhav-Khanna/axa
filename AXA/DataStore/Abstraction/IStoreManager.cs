using System;
using System.Threading.Tasks;
using AXA.DataStore.Abstraction.Stores;

namespace AXA.DataStore.Abstraction
{
    public interface IStoreManager
    {
        IPublicationStore PublicationStore { get; }

        IConfigurationStore ConfigurationStore { get; }

        Task<bool> LoginAsync(string username, string password);

        Task<bool> RegisterAsync(string email,string password,string company,string firstname,string lastname);

        Task<bool> LogoutAsync();

        Task<bool> VerifyToken();
    }
}
