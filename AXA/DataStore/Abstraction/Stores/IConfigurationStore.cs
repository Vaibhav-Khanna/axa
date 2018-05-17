using System;
using System.Threading.Tasks;
using AXA.Models;
using System.Collections.Generic;

namespace AXA.DataStore.Abstraction.Stores
{
    public interface IConfigurationStore : IBaseStore<Configuration>
    {
		Task<List<Tuple<string, string>>> GetCategories();

        Task<Configuration> GetConfiguration(bool refresh = false);

        Task<IEnumerable<Tuple<MenuMenu,bool,bool>>> GetSubscriptionList();

        Task<LoginResponse> GetLoginResponse();

        Task<bool> UpdateSubscriptionDetail(LoginResponse response);

        Task<bool> IsEmailAlreadyInUse(string email);
    }
}
