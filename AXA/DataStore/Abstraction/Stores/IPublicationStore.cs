using System;
using AXA.Models.DataObjects;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AXA.DataStore.Abstraction.Stores
{
    public interface IPublicationStore : IBaseStore<Publication>
    {
        Task<IEnumerable<Publication>> GetPublicationsByCategoryId(string publicationCategoryId, bool refresh = false);

        Task<byte[]> DownloadPublication(Publication publication,bool IsPreview = false);

        Task<byte[]> FetchPublication(Publication publication);

        Task<bool> ArchivePublication(Publication publication);

        Task<IEnumerable<Publication>> GetDownloadedPublications();

		Task<bool> LikePublication(string id);

		Task<bool> SavePublication(Publication publication);
    }
}
